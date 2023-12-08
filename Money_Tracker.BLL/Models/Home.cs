using Money_Tracker.DAL.Entities;


namespace Money_Tracker.BLL.Models
{
    /// <summary>
    /// Représente une entité "Home" contenant des informations sur une maison.
    /// </summary>
    public class Home
    {
        /// <summary>
        /// Obtient ou définit l'identifiant de la maison.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Obtient ou définit l'identifiant de l'utilisateur associé à la maison.
        /// </summary>
        public int User_Id { get; set; }

        /// <summary>
        /// Obtient ou définit le nom de la maison.
        /// </summary>
        public string Name_Home { get; set; } = string.Empty;

        /// <summary>
        /// Obtient ou définit une collection d'utilisateurs associés à la maison.
        /// </summary>
        public IEnumerable<HomeUser> Users { get; set; }
    }
}