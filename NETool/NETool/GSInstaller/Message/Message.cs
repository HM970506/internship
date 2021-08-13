using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SI.NE.CMM.Logging;
using log4net;


namespace GSIntaller
{
    public class log
    {
        public static LogMessageService logService = new LogMessageService();

        public static void logSetting()
        {
            log4net.Config.BasicConfigurator.Configure();

            LogGenerator.ShowTrace = true;
            LogGenerator.ProgramName = "GSInstaller";
            string log_path = System.Windows.Forms.Application.StartupPath;

            LogAppenderManager appenderManager = new LogAppenderManager
            {
                LogPort = 10001,
                ProgramName = "GSInstaller",
                LogFilePath = log_path
            };
            appenderManager.InitLogAppender();

            log_path = @Path.GetDirectoryName(Path.GetDirectoryName(log_path)) + "\\LogMessages.csv";
            log.logService.LoadMessages(log_path);
        }
    }


    /// <summary>
    /// 진행과 무관한 textbox message 클래스
    /// </summary>
    public static class Message
    {
        public static string MainFormNullexist(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-99001"), string.Format(log.logService.GetEnglishMessage("INST-99001"), "input: null Value exist"));
            return "input: null Value exist";
        }

        public static string MainFormUnziperror(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-99002"), string.Format(log.logService.GetEnglishMessage("INST-99002"), "unzip error"));
            return "unzip error";
        }

        public static string MainFormCheck1(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-99003"), string.Format(log.logService.GetEnglishMessage("INST-99003"), "is not backup zip"));
            return "is not backup zip!";
        }
        public static string MainFormCheck2(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-99004"), string.Format(log.logService.GetEnglishMessage("INST-99004"), "is backup zip"));
            return "is backup zip!";
        }
        public static string MainFormCheck3(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-99005"), string.Format(log.logService.GetEnglishMessage("INST-99005"), "Same subsystem in list!"));
            return "Same subsystem in list!";
        }

        public static string MainFormErrorInfo1(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-99006"), string.Format(log.logService.GetEnglishMessage("INST-99006"), "FolderCheck error"));
            return "FolderCheck error";
        }
        public static string MainFormErrorInfo2(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-99007"), string.Format(log.logService.GetEnglishMessage("INST-99007"), "Backup error"));
            return "Backup error";
        }
        public static string MainFormErrorInfo3(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-99008"), string.Format(log.logService.GetEnglishMessage("INST-99008"), "Install error"));
            return "Install error";
        }
        public static string MainFormErrorInfo4(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-99009"), string.Format(log.logService.GetEnglishMessage("INST-99009"), "Makeinstallinformation error"));
            return "Makeinstallinformation error";
        }
        public static string MainFormErrorInfo5(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-99010"), string.Format(log.logService.GetEnglishMessage("INST-99010"), "TempDelete error"));
            return "TempDelete error";
        }
        public static string MainFormErrorInfo6(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-99011"), string.Format(log.logService.GetEnglishMessage("INST-99011"), "Program cancel"));
            return "Program cancel";
        }

        public static string MainFormCancel(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00000"), string.Format(log.logService.GetEnglishMessage("INST-00000"), "canceling... please wait"));
            return "canceling... please wait";
        }

        public static string ServerIdPwInputNullerror(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-99012"), string.Format(log.logService.GetEnglishMessage("INST-99012"), "Empty cell exist!"));
            return "Empty cell exist!";
        }

        public static string Newline()
        {
            return "\r\n";
        }

        public static string DoubleNewline()
        {
            return "\r\n\r\n";
        }


        public static string InstallStart(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00001"), string.Format(log.logService.GetEnglishMessage("INST-00001"), "Program Start"));
            return "Program Start";
        }
        public static string InstallEnd(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00002"), string.Format(log.logService.GetEnglishMessage("INST-00002"), "Program End"));
            return "Program End";
        }
        public static string StartSetting(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00003"), string.Format(log.logService.GetEnglishMessage("INST-00003"), "Setting Start"));
            return "Setting Start";
        }
        public static string StartSettingEnd(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00004"), string.Format(log.logService.GetEnglishMessage("INST-00004"), "Setting End"));
            return "Setting End";
        }

        public static string InstallWorkStart(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00005"), string.Format(log.logService.GetEnglishMessage("INST-00005"), "Install Start"));
            return "Install Start";
        }
        public static string InstallWorkEnd(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00006"), string.Format(log.logService.GetEnglishMessage("INST-00006"), "Install End"));
            return "Install End";
        }

        public static string FolderCheck(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00007"), string.Format(log.logService.GetEnglishMessage("INST-00007"), "Zip Exist Check Start"));
            return "Zip Exist Check Start";
        }
        public static string FolderCheckEnd(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00008"), string.Format(log.logService.GetEnglishMessage("INST-00008"), "Zip Exist Check End"));
            return "Zip Exist Check End";
        }

        public static string DownloadEnd(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00009"), string.Format(log.logService.GetEnglishMessage("INST-00009"), "Install End"));
            return "Install End";
        }

        public static string Backupfolder(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00010"), string.Format(log.logService.GetEnglishMessage("INST-00010"), "Backup Folder Check Start"));
            return "Backup Folder Check Start";
        }
        public static string BackupfolderEnd(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00011"), string.Format(log.logService.GetEnglishMessage("INST-00011"), "Backup Folder Check End"));
            return "Backup Folder Check End";
        }
        public static string Zip(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00012"), string.Format(log.logService.GetEnglishMessage("INST-00012"), "Backup Start"));
            return "Backup Start";
        }
        public static string ZipEnd(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00013"), string.Format(log.logService.GetEnglishMessage("INST-00013"), "Backup End"));
            return "Backup End";
        }

        public static string HashMake(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00014"), string.Format(log.logService.GetEnglishMessage("INST-00014"), "Hash Make Start"));
            return "Hash Make Start";
        }

        public static string HashMakeEnd(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00015"), string.Format(log.logService.GetEnglishMessage("INST-00015"), "Hash Make End"));
            return "Hash Make End";
        }

        public static string HashCheck(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00016"), string.Format(log.logService.GetEnglishMessage("INST-00016"), "Hash Check Start"));
            return "Hash Check Start";
        }

        public static string HashCheckEnd(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00017"), string.Format(log.logService.GetEnglishMessage("INST-00017"), "Hash Check End"));
            return "Hash Check End";
        }

        public static string HashCheckFail(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00018"), string.Format(log.logService.GetEnglishMessage("INST-00018"), "Hash Check Fail"));
            return "Hash Check Fail";
        }

        public static string FolderCheckClientExist(string subsystem, ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00019"), string.Format(log.logService.GetEnglishMessage("INST-00019"), subsystem + " Client exist"));
            return subsystem + " Client exist";
        }

        public static string FolderCheckClientNotExist(string subsystem, ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00020"), string.Format(log.logService.GetEnglishMessage("INST-00020"), subsystem + " Client unexist"));
            return subsystem + " Client unexist";
        }
        public static string FolderCheckServerExist(string subsystem, ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00021"), string.Format(log.logService.GetEnglishMessage("INST-00021"), subsystem + " Server exist"));
            return subsystem + " Server exist";
        }
        public static string FolderCheckServerNotExist(string subsystem, ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00022"), string.Format(log.logService.GetEnglishMessage("INST-00022"), subsystem + " Server unexist"));
            return subsystem + " Server unexist";
        }

        public static string BackupfolderNotExist(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00023"), string.Format(log.logService.GetEnglishMessage("INST-00023"), "Not Backup flie!"));
            return "Not Backup flie!";
        }

        public static string MakeinstallinformationStart(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00024"), string.Format(log.logService.GetEnglishMessage("INST-00024"), "Make installinformation text file start"));
            return "Make installinformation text file start";
        }

        public static string MakeinstallinformationEnd(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00025"), string.Format(log.logService.GetEnglishMessage("INST-00025"), "Make installinformation text file End"));
            return "Make installinformation text file End";
        }

        public static string TempDeleteStart(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00026"), string.Format(log.logService.GetEnglishMessage("INST-00026"), "Temp folder Delete Start"));
            return "Temp folder Delete Start";
        }

        public static string TempDeleteEnd(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00027"), string.Format(log.logService.GetEnglishMessage("INST-00027"), "Temp folder Delete End"));
            return "Temp folder Delete End";
        }

        public static string TempDeleteArleady(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00028"), string.Format(log.logService.GetEnglishMessage("INST-00028"), "Temp folder Delete Arleady"));
            return "Temp folder Delete Arleady";
        }

        public static string ServerConnect(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00029"), string.Format(log.logService.GetEnglishMessage("INST-00029"), "Server Connect Start"));
            return "Server Connect Start";
        }
        public static string ServerUnconnect(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00030"), string.Format(log.logService.GetEnglishMessage("INST-00030"), "Server Connect End"));
            return "Server Connect End";
        }

        public static string FolderCheckError(string subsystem, ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00031"), string.Format(log.logService.GetEnglishMessage("INST-00031"), subsystem + " FolderCheck error: pass"));
            return subsystem + " FolderCheck error: pass";
        }

        public static string MakeInstallinformationError(string subsystem, ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00032"), string.Format(log.logService.GetEnglishMessage("INST-00032"), subsystem + " Make Installinformation Error: pass"));
            return subsystem + " Make Installinformation Error: pass";
        }

        public static string InstallError(string subsystem, ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00033"), string.Format(log.logService.GetEnglishMessage("INST-00033"), subsystem + " Install Error: pass"));
            return subsystem + " Install Error: pass";
        }

        public static string InstallFontInstallStart(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00035"), string.Format(log.logService.GetEnglishMessage("INST-00035"), "Font Install Start"));
            return "Font Install Start";
        }

        public static string InstallFontInstallError(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00036"), string.Format(log.logService.GetEnglishMessage("INST-00036"), "Font Install Error"));
            return "Font Install Error";
        }
        public static string InstallFontInstallEnd(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00037"), string.Format(log.logService.GetEnglishMessage("INST-00037"), "Font Install End"));
            return "Font Install End";
        }

        public static string InstallDataInstallStart(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00038"), string.Format(log.logService.GetEnglishMessage("INST-00038"), "Data Install Start"));
            return "Data Install Start";
        }


        public static string InstallDataInstallError(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00039"), string.Format(log.logService.GetEnglishMessage("INST-00039"), "Data Install Error"));
            return "Data Install Error";
        }

        public static string InstallDataInstallEnd(ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00040"), string.Format(log.logService.GetEnglishMessage("INST-00040"), "Data Install End"));
            return "Data Install End";
        }

        public static string DataInstallError(string subsystem, ILog logger)
        {
            LogGenerator.Log(logger, log.logService.GetLogLevel("INST-00033"), string.Format(log.logService.GetEnglishMessage("INST-00033"), subsystem + " DataInstall Error: pass"));
            return subsystem + " DataInstall Error: pass";
        }

    }
}
