namespace BookLibrary.Models
{
	public class Book
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Author { get; set; }

        #region Foreign keys
		public int? BookTypeId { get; set; }
        public int UserId { get; set; }
        #endregion

        #region Navigation properties
        public virtual BookType BookType { get; set; }
        public virtual User User { get; set; }
        #endregion
    }

}