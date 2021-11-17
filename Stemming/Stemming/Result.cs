namespace Stemming
{
    public class Result
    {
        public readonly bool IsFound;
        public readonly int StartIndex;
        public readonly int EndIndex;

        public Result(bool isFound, int start = 0, int end = 0)
        {
            IsFound = isFound;
            StartIndex = start;
            EndIndex = end;
        }

        protected bool Equals(Result other)
        {
            return IsFound == other.IsFound && StartIndex == other.StartIndex && EndIndex == other.EndIndex;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Result) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = IsFound.GetHashCode();
                hashCode = (hashCode * 397) ^ StartIndex;
                hashCode = (hashCode * 397) ^ EndIndex;
                return hashCode;
            }
        }
    }
}