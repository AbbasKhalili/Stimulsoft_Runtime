namespace stimulTest
{
    public interface IRBuilder
    {
        IHeader WithHeader();
        IFooter WithFooter();
        string Build();
    }

    public interface IHeader : IRBuilder
    {
        IHeader WithVar();
        IHeader WithConst();
    }
    public interface IFooter : IRBuilder
    {
        IFooter WithSum();
    }

    public abstract class BuilderBase
    {
        public IHeader WithHeader()
        {
            throw new System.NotImplementedException();
        }

        public IFooter WithFooter()
        {
            throw new System.NotImplementedException();
        }

        public string Build()
        {
            return "ok";
        }
    }

    public class RBuilder : BuilderBase, IRBuilder
    {
        
    }

    public class Header : BuilderBase,IHeader
    {
        public IHeader WithVar()
        {
            return this;
        }

        public IHeader WithConst()
        {
            return this;
        }
    }

    public class Footer : BuilderBase, IFooter
    {
        public IFooter WithSum()
        {
            throw new System.NotImplementedException();
        }
    }

    public class Client
    {
        public void Build()
        {
            var t = new RBuilder()
                .WithHeader().WithConst().WithVar()
                .WithFooter().WithSum();
        }
    }
}
