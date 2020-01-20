namespace Billas.Identifier
{
    public class PersonIdentifierFormatOption
    {
        /// <summary>
        /// Set to true to force usage of century, or false to force not to use century.
        /// With no value century is used only if century was supplied in first place. 
        /// </summary>
        public bool? WithCentury { get; set; }

        /// <summary>
        /// True to have have separation between the century and the rest of the personal identity.
        /// </summary>
        public bool CenturySeparated { get; set; } = false;

        /// <summary>
        /// Assign character (string) to use as a sepration char. Maybe a blank?
        /// Only used if <see cref="CenturySeparated"/> is true.
        /// </summary>
        public string CenturySeparationChar { get; set; }

        /// <summary>
        /// True to include a hyphen (dash?). Eg YYYYMMDD-NNNN. With no hyphen it is YYYYMMDDNNNN.
        /// If no value is given a hypen is used only if a hyphen was given in the first place.
        /// </summary>
        public bool? WithHyphen { get; set; }

        /// <summary>
        /// The hyphen char/string to use. If no value is supplied the "correct" (+ or -) is used based on age.
        /// </summary>
        public string HyphenChar { get; set; }

        public static PersonIdentifierFormatOption None => new PersonIdentifierFormatOption { WithHyphen = false };

        public static PersonIdentifierFormatOption ForDisplay => new PersonIdentifierFormatOption { WithHyphen = true };
    }
}