namespace DatacircleAPI
{
    public class Email
    {
        public string RecipientEmailAddress { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Template { get; set; }

        override public string ToString() 
        {
            return RecipientEmailAddress + " " + Subject + " " + Message + " " + Template;
        }
    }
}
