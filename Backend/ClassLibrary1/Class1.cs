namespace ClassLibrary1
{
    public interface Itest
    {
        void Flying();
    }
    public interface ISonic
    {
        void SuperSonic();
    }




    public class Bird : Itest
    {
        public void SuperSonic()
        {
            throw new NotImplementedException();
        }

        void Flying()
        {
            Console.WriteLine("wee");
        }
    }

    public class Gripen : Itest
    {
        public void Flying()
        {
            throw new NotImplementedException();
        }

        public void SuperSonic()
        {
            throw new NotImplementedException();
        }
    }
}
