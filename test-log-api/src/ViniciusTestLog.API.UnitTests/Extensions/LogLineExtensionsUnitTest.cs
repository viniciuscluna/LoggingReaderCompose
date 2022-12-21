using ViniciusTestLog.API.Extensions;

namespace ViniciusTestLog.API.UnitTests.Extensions
{
    public class LogLineExtensionsUnitTest
    {
        [Trait("Category", "Unit")]
        [Fact(DisplayName ="Extensions - LogLineExtensions - Shoulp Map string correctly (Normal Date)")]
        public void ShoulpMapStringCorrectlyNormalDate()
        {
            //Arrange
            string logLine = "Nov 30 06:47:03 ip-172-31-27-153 CRON[22087]: pam_unix(cron:session): session closed for user root";
            var expectedDate = new DateTime(2022, 11, 30, 06, 47, 03);
            var expectedIp = "172-31-27-153";
            var expectedCategory = "CRON[22087]";
            var expectedMessage = "pam_unix(cron:session): session closed for user root";

            //Act
            var logData = logLine.ToLogData();

            //Assert
            Assert.Equal(expectedDate, logData.Date);
            Assert.Equal(expectedIp, logData.Ip);
            Assert.Equal(expectedCategory, logData.Category);
            Assert.Equal(expectedMessage, logData.Message);
        }

        [Trait("Category", "Unit")]
        [Fact(DisplayName = "Extensions - LogLineExtensions - Shoulp Map string correctly (One Digit Date)")]
        public void ShoulpMapStringCorrectlyOneDigitDate()
        {
            //Arrange
            string logLine = "Dec  2 13:17:15 ip-172-31-27-153 sshd[6698]: input_userauth_request: invalid user test [preauth]";
            var expectedDate = new DateTime(2022, 12, 02, 13, 17, 15);
            var expectedIp = "172-31-27-153";
            var expectedCategory = "sshd[6698]";
            var expectedMessage = "input_userauth_request: invalid user test [preauth]";

            //Act
            var logData = logLine.ToLogData();

            //Assert
            Assert.Equal(expectedDate, logData.Date);
            Assert.Equal(expectedIp, logData.Ip);
            Assert.Equal(expectedCategory, logData.Category);
            Assert.Equal(expectedMessage, logData.Message);
        }

        [Trait("Category", "Unit")]
        [Fact(DisplayName = "Extensions - LogLineExtensions - Shoulp return null when log is not parsed")]
        public void ShoulpReturnNullWhenLogIsNotParsed()
        {
            //Arrange
            string logLine = "Invalid log";

            //Act
            var logData = logLine.ToLogData();

            //Assert
            Assert.Null(logData);
        }

    }
}
