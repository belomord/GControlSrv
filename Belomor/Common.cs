﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Microsoft.Win32;



namespace Belomor.Common
{
  public static class CommonProc
  {
    public static string ApplicationExeName = Application.ExecutablePath;
    public static string ApplicationExePath = Path.GetDirectoryName(Application.ExecutablePath) + @"\";

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

    public static string FullDTFormatStr => FullDateTimeFormatStr();

    //Получить путь к  установленному приложению
    public static string GetAppPath(string subAppPath)
    {
      return (string) Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\Wow6432Node\"+ subAppPath, "InstallPath", "");
    }


  }
}
