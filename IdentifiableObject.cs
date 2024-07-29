namespace OOP_custom_project
{
    public class IdentifiableObject
    {
        private readonly List<string> _identifiers;

        public IdentifiableObject(string[] idents)
        {
            _identifiers = [];
            for (int i = 0; i < idents.Length; i++)
            {
                _identifiers.Add(idents[i].ToLower());
            }
        }

        public bool AreYou(string id)
        {
            return _identifiers.Contains(id.ToLower());
        }

        public string FirstID
        {
            get
            {
                if (_identifiers.Count == 0)
                {
                    return "";
                }
                else
                {
                    return _identifiers.First();
                }
            }
        }
    }
}
