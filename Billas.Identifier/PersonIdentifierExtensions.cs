using System;

namespace Billas.Identifier
{
    public static class PersonIdentifier
    {
        /// <summary>
        /// Parse a personal identifier.
        /// Can only parse to <see cref="NationalReserveNumberIdentifier"/>, <see cref="CoordinationNumberIdentifier"/> or <see cref="PersonalNumberIdentifier"/>.
        /// </summary>
        /// <param name="value">A string representation of a personal identifier.</param>
        /// <returns>An instance of <see cref="NationalReserveNumberIdentifier"/>, <see cref="CoordinationNumberIdentifier"/> or <see cref="PersonalNumberIdentifier"/></returns>
        public static IPersonIdentifier Parse(string value)
        {
            var formatter = new DateBasedFormatter(value);
            IPersonIdentifier identifier = null;
            if (formatter.Century == 0 || (formatter.Century >= 22 && formatter.Century <= 78 && (formatter.Century - 19) % 3 == 0))
            {
                identifier = new NationalReserveNumberIdentifier(value);
            }
            else if(formatter.HaveValidYear() && formatter.IsAllNumeric())
            {
                if(formatter.Day > 31)
                    identifier = new CoordinationNumberIdentifier(value);
                else
                    identifier = new PersonalNumberIdentifier(value);
            }
                
            if(identifier == null)
                throw new PersonIdentifierFormatException(value, ExceptionMessage.NotAbleToParse);
            
            return identifier;
        }

        public static bool TryParse(string value, out IPersonIdentifier identifier)
        {
            try
            {
                identifier = Parse(value);
                return true;
            }
            catch (Exception)
            {
                identifier = null;
                return false;
            }
        }

        /// <summary>
        /// Load a personal identifier to the type represented by supplied <see cref="oid"/>.
        /// </summary>
        /// <param name="oid">The Oid (domain) for supplied value.</param>
        /// <param name="value">The personal identifier.</param>
        /// <returns>An instance representing the personal identifier. Can be national or any registered local representation.</returns>
        public static IPersonIdentifier Load(string oid, string value)
        {
            var typeLoader = new PersonIdentifierTypeLoader();
            var type = typeLoader.FindType(oid);
            if(type == null)
                throw new TypeLoadException($"Found no type that can handle oid '{oid}'.");

            var ctorWithStringParam = typeLoader.GetConstructorWithStringParameter(type);
            if (ctorWithStringParam != null)
            {
                try
                {
                    return Activator.CreateInstance(type, value) as IPersonIdentifier;
                }
                catch (Exception e)
                {
                    if (e.InnerException != null)
                        throw e.InnerException;
                }
            }


            var ctorWithFormatterParam = typeLoader.GetConstructorWithFormatterParameter(type, out var formatterType);
            if (ctorWithFormatterParam != null)
            {
                try
                {
                    var formatter = Activator.CreateInstance(formatterType, value);
                    return Activator.CreateInstance(type, formatter) as IPersonIdentifier;
                }
                catch (Exception e)
                {
                    if (e.InnerException != null)
                        throw e.InnerException;
                }
            }

            throw new MissingMethodException($"Found type {type.Name} but it had no suitable constructor.");
        }

        public static bool TryLoad(string oid, string value, out IPersonIdentifier identifier)
        {
            try
            {
                identifier = Load(oid, value);
                return true;
            }
            catch (Exception)
            {
                identifier = null;
                return false;
            }
        }
    }
}