namespace NotasRapidas.Model
{
    internal class Note
    {
        private string _title = "Sin título";

        public string Title
        {
            get => _title;
            set => _title = string.IsNullOrWhiteSpace(value) ? _title : value.Trim();
        }

        private string _comment = "Sin Comentario";

        public string Comment
        {
            get => _comment;
            set => _comment = string.IsNullOrWhiteSpace(value) ? _comment : value.Trim();
        }

        public Note(string title, string comment)
        {
            Title = title;
            Comment = comment;
        }
    }
}
