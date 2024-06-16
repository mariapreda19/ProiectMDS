using NUnit.Framework;

public class PlayerEditModeTests
{
    [Test]
    public void Player_Name_Updates_Correctly()
    {
        // Act
        Player.setName("TestPlayer");

        // Assert
        Assert.AreEqual("TestPlayer", Player.getName());
    }

    [Test]
    public void Player_Money_Updates_Correctly()
    {
        // Act
        Player.UpdateMoney(10);

        // Assert
        Assert.AreEqual(10, Player.getMoney());
    }

    [Test]
    public void Player_Score_Updates_Correctly()
    {
        // Act
        Player.UpdateScore(200);

        // Assert
        Assert.AreEqual(200, Player.getScore());
    }
}