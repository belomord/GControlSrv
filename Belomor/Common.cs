using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;
using System.Globalization;


namespace Belomor.Common
{

  public enum BFileDeleteResult 
  {
    Success = 0x0000,

    ArgumentException = 0x0001,
    ArgumentNullException= 0x0002,
    DirectoryNotFoundException= 0x0003,
    IOException= 0x0004,
    NotSupportedException= 0x0005,
    PathTooLongException= 0x0006,
    UnauthorizedAccessException= 0x0007,
  }


  public static class CommonProc
  {
    static CommonProc()
    {
      DecimalCultureInfo = CultureInfo.InvariantCulture.Clone() as CultureInfo;
      DecimalCultureInfo.NumberFormat.NumberDecimalSeparator = ".";
      DecimalCultureInfo.NumberFormat.NumberGroupSeparator = "";
    }


    public static CultureInfo DecimalCultureInfo;

    public static decimal ToDecimal(string str, decimal defaultValue = 0)
    {
      try
      {
        string s = str.Trim().Replace(" ", "");
        return Convert.ToDecimal(str, DecimalCultureInfo); 
      }
      catch 
      {
        return defaultValue;
      }
    }

    public static string DecimalToStr(decimal Value) => Convert.ToString(Value, DecimalCultureInfo);


    public static string ApplicationExeName = Application.ExecutablePath;
    public static string ApplicationExePath = Path.GetDirectoryName(Application.ExecutablePath) + @"\";

    public static Exception DeleteFileE(string FileName)
    {
      string fn = String.IsNullOrEmpty(FileName) || String.IsNullOrWhiteSpace(FileName) ? "" : FileName.Trim();

      Exception result = null;

      try
      {
        if (File.Exists(fn))
          File.Delete(fn);
      }
      catch (Exception e)
      {
        result = e;
      }

      return result;
    }

    public static bool DeleteFile(string FileName)
    {
      return DeleteFileE(FileName) == null;
    }

    public static string FinPath(string path)
    {
      string _path = "";
      if (!String.IsNullOrWhiteSpace(path))
      {
        _path = path.Trim();
        string directorySeparator = Path.DirectorySeparatorChar.ToString();
        _path = _path.EndsWith(directorySeparator) ? _path : _path + directorySeparator;
      }

      return _path;
    }

    public static string RandomString(int size)
    {
      StringBuilder builder = new StringBuilder();
      Random random = new Random();
      char ch;
      for (int i = 0; i < size; i++)
      {
        //Генерируем число являющееся латинским символом в юникоде
        ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
        //Конструируем строку со случайно сгенерированными символами
        builder.Append(ch);
      }
      return builder.ToString();
    }

    public static bool DeleteFiles(string directory, string mask)
    {
      bool res = (directory.Trim()!="") && Directory.Exists(directory);


      if (res)
      {
        string[] files = Directory.GetFiles(directory.Trim(), mask);
        foreach (string file in files)
        {
          try
          {
            File.Delete(file);
          }
          catch
          {
            res = false;
          }
        }

      }

      return res;
    }

    //Форматирование дат и времени:

    //d           - Представляет день месяца от 1 до 31. Одноразрядные числа используются без нуля в начале
    //dd          - Представляет день месяца от 1 до 31. К одноразрядным числам в начале добавляется ноль
    //ddd         - Сокращенное название дня недели
    //dddd        - Полное название дня недели
    //f / fffffff - Представляет миллисекунды. Количество символов f указывает на число разрядов в миллисекундах
    //g           - Представляет период или эру (например, "н. э.")
    //h           - Часы в виде от 1 до 12. Часы с одной цифрой не дополняются нулем
    //hh          - Часы в виде от 01 до 12. Часы с одной цифрой дополняются нулем
    //H           - Часы в виде от 0 до 23. Часы с одной цифрой не дополняются нулем
    //HH          - Часы в виде от 0 до 23. Часы с одной цифрой дополняются нулем
    //K           - Часовой пояс
    //m           - Минуты от 0 до 59. Минуты с одной цифрой не дополняются начальным нулем
    //mm          - Минуты от 0 до 59. Минуты с одной цифрой дополняются начальным нулем
    //M           - Месяц в виде от 1 до 12
    //MM          - Месяц в виде от 1 до 12. Месяц с одной цифрой дополняется начальным нулем
    //MMM         - Сокращенное название месяца
    //MMMM        - Полное название месяца
    //s           - Секунды в виде числа от 0 до 59. Секунды с одной цифрой не дополняются начальным нулем
    //ss          - Секунды в виде числа от 0 до 59. Секунды с одной цифрой дополняются начальным нулем
    //t           - Первые символы в обозначениях AM и PM
    //tt          - AM или PM
    //y           - Представляет год как число из одной или двух цифр. Если год имеет более двух цифр, то в результате отображаются только две младшие цифры
    //yy          - Представляет год как число из одной или двух цифр. Если год имеет более двух цифр, то в результате отображаются только две младшие цифры. Если год имеет одну цифру, то он дополняется начальным нулем
    //yyy         - Год из трех цифр
    //yyyy        - Год из четырех цифр
    //yyyyy       - Год из пяти цифр. Если в году меньше пяти цифр, то он дополняется начальными нулями
    //z           - Представляет смецщение в часах относительно времени UTC
    //zz          - Представляет смецщение в часах относительно времени UTC. Если смещение представляет одну цифру, то она дополняется начальным нулем.    
    public static string FullDateTimeFormatStr(string dateDelimeter, string timeDelimeter, string dateTimeDelimeter, bool useMillisecond)
    {
      string dateStr = "yyyy MM dd";
      string timeStr = "HH mm ss";

      if (useMillisecond)
        timeStr += " fff";

      dateStr = dateStr.Replace(" ", dateDelimeter);
      timeStr = timeStr.Replace(" ", timeDelimeter);

      return dateStr + dateTimeDelimeter + timeStr;
    }

    public static string FullDateTimeFormatStr(string delimeter="-", bool useMillisecond=true)
    {
      return FullDateTimeFormatStr(delimeter, delimeter, delimeter, useMillisecond);
    }

    public static string DateTimeToStr(DateTime dateTime, string dateDelimeter, string timeDelimeter, string dateTimeDelimeter, bool useMillisecond)
    {
      return dateTime.ToString(FullDateTimeFormatStr(dateDelimeter, timeDelimeter, dateTimeDelimeter, useMillisecond));
    }

    public static string DateTimeToStr(DateTime dateTime, string delimeter = "-", bool useMillisecond = true)
    {
      return dateTime.ToString(FullDateTimeFormatStr(delimeter, delimeter, delimeter, useMillisecond));
    }

    public static string FullDTFormatStr => FullDateTimeFormatStr();

    //Получить путь к  установленному приложению
    public static string GetAppPath(string subAppPath)
    {
      return (string) Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\"+ subAppPath, "InstallPath", "");
    }


  }
}
