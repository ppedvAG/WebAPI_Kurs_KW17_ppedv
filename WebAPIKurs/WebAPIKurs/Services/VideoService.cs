namespace WebAPIKurs.Services
{
    public class VideoService : IVideoService
    {
        private HttpClient _httpClient;

        //Via IHttpClientFactory erhalten wir eine HttpClient - Instanz
        public VideoService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Stream> GetVideoByName(string name)
        {
            //using (HttpClient httpClient = _httpClient) //IDispose-Methode 
            //{
            //} //httpClient wird aufgeräumt -> er greift auf eine Socket (das verzögert) 

            string url = string.Empty;

            switch (name)
            {
                case "fugees":
                    url = "http://gartner.gosimian.com/assets/videos/Fugees_ReadyOrNot_278-WIREDRIVE.mp4";
                    break;
                case "xyz":
                    url = "http://gartner.gosimian.com/assets/videos/George_Michael_MV-WIREDRIVE.mp4";
                    break;
                default:
                    url = "abc";
                    break;
            }

            return await _httpClient.GetStreamAsync(url);

        }
    }
}
