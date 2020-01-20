using System.Linq;

namespace Billas.Identifier.VGR
{
    public class VGRIdentifier : DateBasedPersonIdentifier<VGRFormatter>
    {
        public const string Oid = "1.2.752.113.11.0.2.1.1.1";
        public override string System => Oid;
        public override string DisplayName => "Västra Götalandsregionen";

        public override bool CanCalculateGender { get; }
        public override PersonIdentityGender CalculatedGender { get; }

        public VGRIdentifier(string value)
            : this(new VGRFormatter(value)) { }

        public VGRIdentifier(VGRFormatter formatter)
            : base(formatter)
        {
            CanCalculateGender = true;
            var gender = VGRFormatter.GenderMap.FirstOrDefault(x => x.Letter == formatter.GenderIndicator);
            CalculatedGender =  gender?.Gender ?? PersonIdentityGender.Unknown;
        }
    }
}