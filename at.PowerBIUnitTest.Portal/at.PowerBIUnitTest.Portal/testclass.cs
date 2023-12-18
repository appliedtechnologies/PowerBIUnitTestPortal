using Xunit;
namespace at.PowerBIUnitTest.Portal{
public class testclass
{
    [Fact]
    public void PassingAddTest(){
    Assert.Equal(4,Adds.Add(2,2));

    
}



}
}