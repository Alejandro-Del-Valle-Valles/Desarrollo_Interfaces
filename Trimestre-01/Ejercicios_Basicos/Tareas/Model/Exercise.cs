using System.Text;

namespace Tareas.Model
{
    internal class Exercise
    {
        public string Title { get; private set; }
        public string Comment { get; private set; }
        public DateTime DeliveryDate { get; private set; }

        public Exercise(string title, string comment, DateTime deliveryDate)
        {
            Title = title;
            Comment = comment;
            DeliveryDate = deliveryDate;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Title.Trim())
                .Append('#')
                .Append(string.IsNullOrWhiteSpace(Comment) ? Comment.Trim() : "Sin comentarios.")
                .Append('#')
                .Append(DeliveryDate.ToString("dd/MM/yyyy"));
            return sb.ToString();
        }
    }
    
}
