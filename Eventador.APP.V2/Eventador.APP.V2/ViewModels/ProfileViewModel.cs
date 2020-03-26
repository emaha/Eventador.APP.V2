namespace Eventador.APP.V2.ViewModels
{
    public class ProfileViewModel
    {

        /// <summary>
        /// 
        /// </summary>
        public long UserId { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string FullName { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public string ContactPhone { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        public float Rating { get; set; }

        /// <summary>
        /// О себе
        /// </summary>
        public string Bio { get; set; }

        public ProfileViewModel()
        {

        }


    }
}