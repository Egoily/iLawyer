using System;
using System.Globalization;
using System.Text;

namespace ee.iLawyer.Domain
{
    internal class ChineseLunisolarCalendarWithFestival : ChineseLunisolarCalendar
    {
        public DateTime CurrentDateTime { get; private set; }

        private int weekValue = -1;
        public int WeekValue
        {
            get
            {
                if (weekValue == -1)
                {
                    weekValue = CaculateWeekDay(CurrentDateTime.Year, CurrentDateTime.Month, CurrentDateTime.Day);
                }
                return weekValue;

            }
        }
        /// <summary>
        /// 获得星期(中文)
        /// </summary>
        public string ChineseWeek
        {
            get
            {
                var chineseWeekValue = "";
                switch (WeekValue)
                {
                    case 0:
                    case 7:
                        chineseWeekValue = "星期日";
                        break;
                    case 1:
                        chineseWeekValue = "星期一";
                        break;
                    case 2:
                        chineseWeekValue = "星期二";
                        break;
                    case 3:
                        chineseWeekValue = "星期三";
                        break;
                    case 4:
                        chineseWeekValue = "星期四";
                        break;
                    case 5:
                        chineseWeekValue = "星期五";
                        break;
                    case 6:
                        chineseWeekValue = "星期六";
                        break;
                }
                return chineseWeekValue;
            }
        }


        /// <summary>
        /// 获得月内的第几周
        /// </summary>
        public int WeekNoOfMonth
        {
            get
            {
                return GetWeekNoOfMonth(CurrentDateTime.Year, CurrentDateTime.Month, CurrentDateTime.Day);
            }
        }
        /// <summary>
        /// 获得农历节日
        /// </summary>
        public string LunarFestival
        {
            get
            {
                return GetLunarFestival(LunarMonth, LunarDay);
            }
        }
        /// <summary>
        /// 获得公历节日
        /// </summary>
        public string GregorianCalendarFestival
        {
            get
            {
                return GetGregorianCalendarFestival(CurrentDateTime.Month, CurrentDateTime.Day);
            }
        }
        /// <summary>
        /// 获得世界节日
        /// </summary>
        public string WorldFestival
        {
            get
            {
                var noOfWeekMonth = GetNoOWeekfMonth(CurrentDateTime.Year, CurrentDateTime.Month, CurrentDateTime.Day);
                return GetWorldFestival(CurrentDateTime.Month, noOfWeekMonth, WeekValue);
            }
        }

        public string FestivalString
        {
            get
            {
                var festival = LunarFestival;
                if (!string.IsNullOrEmpty(GregorianCalendarFestival))
                {
                    if (!string.IsNullOrEmpty(festival))
                    {
                        festival += "|";
                    }
                    festival += $"{GregorianCalendarFestival}";
                }
                if (!string.IsNullOrEmpty(WorldFestival))
                {
                    if (!string.IsNullOrEmpty(festival))
                    {
                        festival += "|";
                    }
                    festival += $"{WorldFestival}";
                }

                return festival;
            }
        }

        /// <summary>
        /// 获得农历年
        /// </summary>
        public string LunarYearValue
        {
            get
            {
                return ToLunaryYear(LunarYear);
            }
        }
        /// <summary>
        /// 获得农历月
        /// </summary>
        public string LunarMonthValue
        {
            get
            {
                if (LeapMonth > 0)
                {
                    return "闰" + lunarMonthName[LunarMonth - 1];
                }
                else
                {
                    return lunarMonthName[LunarMonth - 1];
                }
            }
        }
        /// <summary>
        /// 获得农历日
        /// </summary>
        public string LunarDayValue
        {
            get
            {
                return ToLunaryDay(LunarDay);
            }
        }
        /// <summary>
        /// 获得干支农历年
        /// </summary>
        public string LunarGanZhiValue
        {
            get
            {
                return Cyclical(LunarYear);
            }
        }
        /// <summary>
        /// 获得生肖农历年
        /// </summary>
        public string LunarZodiacValue
        {
            get
            {
                return Zodiac(LunarYear);
            }
        }




        #region //构造方法
        public ChineseLunisolarCalendarWithFestival()
        {
            CurrentDateTime = DateTime.Today;
            Init();
        }

        public ChineseLunisolarCalendarWithFestival(DateTime dt)
        {
            CurrentDateTime = dt;
            Init();
        }

        public int LunarYear { get; private set; }
        public int LunarMonth { get; private set; }
        public int LunarDay { get; private set; }
        public int LeapMonth { get; private set; }
        private void Init()
        {

            LunarYear = GetYear(CurrentDateTime);
            LunarMonth = GetMonth(CurrentDateTime);
            LunarDay = GetDayOfMonth(CurrentDateTime);
            LeapMonth = GetLeapMonth(CurrentDateTime.Year);

        }
        #endregion


        #region 私有成员　属性



        #endregion


        #region //静态数据



        private static readonly string[] Gan = { "甲", "乙", "丙", "丁", "戊", "己", "庚", "辛", "壬", "癸" };

        private static readonly string[] Zhi = { "子", "丑", "寅", "卯", "辰", "巳", "午", "未", "申", "酉", "戌", "亥" };

        private static readonly string[] Zodiacs = { "鼠", "牛", "虎", "兔", "龙", "蛇", "马", "羊", "猴", "鸡", "狗", "猪" };

        private static readonly string[] solarTerm = { "小寒", "大寒", "立春", "雨水", "惊蛰", "春分", "清明", "谷雨", "立夏", "小满", "芒种", "夏至", "小暑", "大暑", "立秋", "处暑", "白露", "秋分", "寒露", "霜降", "立冬", "小雪", "大雪", "冬至" };


        private static readonly string[] ChineseMonthPrefix = { "初", "十", "廿", "卅", "□" };

        private static readonly string[] ChineseNumers = { "零", "一", "二", "三", "四", "五", "六", "七", "八", "九", "十" };



        private static readonly string[] monthName = { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };

        private static readonly string[] lunarMonthName = { "正月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一", "腊月" };

        //国历节日 *表示放假日
        private static string[] sFtv = {"0101*元旦","0202 世界湿地日","0210 国际气象节","0214 情人节","0301 国际海豹日","0303 全国爱耳日",
                                      "0305 学雷锋纪念日","0308 妇女节","0312 植树节 孙中山逝世纪念日","0314 国际警察日","0315 消费者权益日",
                                      "0317 中国国医节 国际航海日","0321 世界森林日 消除种族歧视国际日 世界儿歌日","0322 世界水日",
                                      "0323 世界气象日","0324 世界防治结核病日","0330 巴勒斯坦国土日","0401 愚人节 全国爱国卫生运动月(四月) 税收宣传月(四月)",
                                     "0407 世界卫生日",
                                     "0422 世界地球日",
                                     "0423 世界图书和版权日",
                                     "0424 亚非新闻工作者日",
                                     "0501*劳动节",
                                     "0504 青年节",
                                     "0505 碘缺乏病防治日",
                                     "0508 世界红十字日",
                                     "0512 国际护士节",
                                     "0515 国际家庭日",
                                     "0517 国际电信日",
                                     "0518 国际博物馆日",
                                     "0520 全国学生营养日",
                                     "0523 国际牛奶日",
                                     "0531 世界无烟日",
                                     "0601 国际儿童节",
                                     "0605 世界环境保护日",
                                     "0606 全国爱眼日",
                                     "0617 防治荒漠化和干旱日",
                                     "0623 国际奥林匹克日",
                                     "0625 全国土地日",
                                     "0626 国际禁毒日",
                                     "0701 香港回归纪念日 中共诞辰 世界建筑日",
                                     "0702 国际体育记者日",
                                     "0707 抗日战争纪念日",
                                     "0711 世界人口日",
                                     "0730 非洲妇女日",
                                     "0801 建军节",
                                     "0808 中国男子节(爸爸节)",
                                     "0815 抗日战争胜利纪念",
                                     "0903 中国抗日战争胜利纪念日",
                                     "0908 国际扫盲日 国际新闻工作者日",
                                     "0909 毛泽东逝世纪念",
                                     "0910 中国教师节",
                                     "0914 世界清洁地球日",
                                     "0916 国际臭氧层保护日",
                                     "0918 九·一八事变纪念日",
                                     "0920 国际爱牙日",
                                     "0927 世界旅游日",
                                     "0928 孔子诞辰",
                                     "1001*国庆节 世界音乐日 国际老人节",
                                     "1002*国庆节假日 国际和平与民主自由斗争日",
                                     "1003*国庆节假日",
                                     "1004 世界动物日",
                                     "1006 老人节",
                                     "1008 全国高血压日 世界视觉日",
                                     "1009 世界邮政日 万国邮联日",
                                     "1010 辛亥革命纪念日 世界精神卫生日",
                                     "1013 世界保健日 国际教师节",
                                     "1014 世界标准日",
                                     "1015 国际盲人节(白手杖节)",
                                     "1016 世界粮食日",
                                     "1017 世界消除贫困日",
                                     "1022 世界传统医药日",
                                     "1024 联合国日",
                                     "1031 世界勤俭日",
                                     "1107 十月社会主义革命纪念日",
                                     "1108 中国记者日",
                                     "1109 全国消防安全宣传教育日",
                                     "1110 世界青年节",
                                     "1111 国际科学与和平周(本日所属的一周)",
                                     "1112 孙中山诞辰纪念日",
                                     "1114 世界糖尿病日",
                                     "1117 国际大学生节 世界学生节",
                                     "1120*彝族年",
                                     "1121*彝族年 世界问候日 世界电视日",
                                     "1122*彝族年",
                                     "1129 国际声援巴勒斯坦人民国际日",
                                     "1201 世界艾滋病日",
                                     "1203 世界残疾人日",
                                     "1205 国际经济和社会发展志愿人员日",
                                     "1208 国际儿童电视日",
                                     "1209 世界足球日",
                                     "1210 世界人权日",
                                     "1212 西安事变纪念日",
                                     "1213 南京大屠杀(1937年)纪念日！紧记血泪史！",
                                     "1220 澳门回归纪念",
                                     "1221 国际篮球日",
                                     "1224 平安夜",
                                     "1225 圣诞节",
                                     "1226 毛泽东诞辰纪念"
                                     };

        //农历节日 *表示放假日
        private static string[] lFtv = {
                                     "0101*春节",
                                     "0102*春节",
                                     "0103*春节",
                                     "0115 元宵节",
                                     "0505 端午节",
                                     "0624*火把节",
                                     "0625*火把节",
                                     "0626*火把节",
                                     "0707 七夕情人节",
                                     "0715 中元节",
                                     "0815 中秋节",
                                     "0909 重阳节",
                                     "1208 腊八节",
                                     "1224 小年",
                                     "0100 除夕"
                                      };

        //某月的第几个星期几
        private static string[] twFtv = {
                                     "0150 世界麻风日", //一月的最后一个星期日（月倒数第一个星期日）
                                     "0351 全国中小学生安全教育日",
                                     "0520 国际母亲节",
                                     "0530 全国助残日",
                                     "0630 父亲节",
                                     "0730 被奴役国家周",
                                     "0932 国际和平日",
                                     "0940 国际聋人节 世界儿童日",
                                     "0950 世界海事日",
                                     "1011 国际住房日",
                                     "1013 国际减轻自然灾害日(减灾日)",
                                     "1144 感恩节"
                                   };


        #endregion


        #region//日期计算方法


        /// <summary>
        /// 传入　传入农历年 返回干支, 0=甲子
        /// </summary>
        /// <param name="lunarYear"></param>
        /// <returns></returns>
        public string Cyclical(int lunarYear)
        {
            return (Gan[(lunarYear - 4) % 60 % 10] + Zhi[(lunarYear - 4) % 60 % 12]);
        }

        /// <summary>　
        /// 传入农历年 返回干支, 0=鼠
        /// </summary>
        /// <param name="lunarYear"></param>
        /// <returns></returns>
        public string Zodiac(int lunarYear)
        {
            return Zodiacs[(lunarYear - 4) % 60 % 12];
        }

        public static string ToLunaryDay(int day)
        {
            string s;

            switch (day)
            {
                case 10:
                    s = "初十";
                    break;
                case 20:
                    s = "二十";
                    break;
                case 30:
                    s = "三十";
                    break;
                default:
                    s = ChineseMonthPrefix[day / 10];
                    s += ChineseNumers[day % 10];
                    break;
            }
            return (s);
        }

        /// <summary>
        /// 格式化中文年
        /// </summary>
        /// <param name="d"></param>
        /// <returns></returns>
        public static string ToLunaryYear(int year)
        {
            var sb = new StringBuilder();
            int quotient = year;
            if (quotient > 0)
            {
                quotient = quotient / 10;
                var index = quotient % 10;
                sb.Insert(0, ChineseNumers[index]);
            }
            return sb.ToString();
        }
        /// <summary>
        /// 期根据年月日计算星期几
        /// </summary>
        /// <param name="y"></param>
        /// <param name="m"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static int CaculateWeekDay(int y, int m, int d) //y－年，m－月，d－日,期根据年月日计算星期几的函数
        {
            if (m == 1)
            {
                m = 13;
                y = y - 1;
            }
            if (m == 2)
            {
                m = 14;
                y = y - 1;
            }
            int week = (d + 2 * m + 3 * (m + 1) / 5 + y + y / 4 - y / 100 + y / 400) % 7; //基姆拉尔森计算公式
            int weekstr = 0;
            switch (week)
            {
                case 0: weekstr = 1; break;
                case 1: weekstr = 2; break;
                case 2: weekstr = 3; break;
                case 3: weekstr = 4; break;
                case 4: weekstr = 5; break;
                case 5: weekstr = 6; break;
                case 6: weekstr = 7; break;
            }
            return weekstr;
        }

        /// <summary>
        /// 计算是该月第几周
        /// </summary>
        /// <param name="y"></param>
        /// <param name="m"></param>
        /// <param name="d"></param>
        /// <returns></returns>
        public static int GetWeekNoOfMonth(int y, int m, int d)
        {
            int weekNo = 0;

            int week = CaculateWeekDay(y, m, d);

            if (week == 7)
            {
                week = 0;
            }

            if (d > (week))
            {
                if ((d - week) % 7 == 0)
                {
                    weekNo = (d - week) / 7 + 1;
                }
                else
                {
                    weekNo = (d - week) / 7 + 2;
                }
            }
            else
            {
                weekNo = 1;
            }
            return weekNo;

        }
        public static int GetNoOWeekfMonth(int y, int m, int d)
        {
            int weekNo = 0;

            int week = CaculateWeekDay(y, m, d);

            if (week == 7)
            {
                week = 0;
            }

            if (d > (week))
            {
                if ((d - week) % 7 == 0)
                {
                    weekNo = (d - week) / 7 + 1;
                }
                else
                {
                    weekNo = (d - week) / 7 + 2;
                }
            }
            else
            {
                weekNo = 1;
            }

            int firstWeekDayOfMonth = CaculateWeekDay(y, m, 1);

            if (week < firstWeekDayOfMonth)
            {
                weekNo = weekNo - 1;
            }
            return weekNo;

        }
        #endregion





        #region 节假日计算
        /// <summary>
        /// 公历日期返回公历节假日
        /// </summary>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public static string GetGregorianCalendarFestival(int month, int day)
        {
            string str = @"(\d{2})(\d{2})([\s\*])(.+)$";  //匹配的正则表达式

            System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex(str);

            for (int i = 0; i < sFtv.Length; i++)
            {
                string[] s = re.Split(sFtv[i]);

                if (Convert.ToInt32(s[1]) == month && Convert.ToInt32(s[2]) == day)
                {

                    return s[4];
                }

            }
            return "";

        }

        /// <summary>
        /// 农历日期返回农历节日
        /// </summary>
        /// <param name="lunarMonth"></param>
        /// <param name="Lunarday"></param>
        /// <returns></returns>
        public static string GetLunarFestival(int lunarMonth, int lunarDay)
        {
            string str = @"(\d{2})(\d{2})([\s\*])(.+)$";  //匹配的正则表达式

            System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex(str);

            for (int i = 0; i < lFtv.Length; i++)
            {
                string[] s = re.Split(lFtv[i]);

                if (Convert.ToInt32(s[1]) == lunarMonth && Convert.ToInt32(s[2]) == lunarDay)
                {

                    return s[4];
                }

            }
            return "";

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="month"></param>
        /// <param name="weekNoOfMonth">该月第几周</param>
        　　 ///  <param name="week">周几</param>
        /// <returns></returns>
        public static string GetWorldFestival(int month, int weekNoOfMonth, int week)
        {
            string str = @"(\d{2})(\d{1})(\d{1})([\s\*])(.+)$";  //匹配的正则表达式

            System.Text.RegularExpressions.Regex re = new System.Text.RegularExpressions.Regex(str);

            if (week == 7)
            {
                week = 0;
            }

            for (int i = 0; i < twFtv.Length; i++)
            {
                string[] s = re.Split(twFtv[i]);

                var w = Convert.ToInt32(s[3]);
                if (w == 7)
                {
                    w = 0;
                }

                if (Convert.ToInt32(s[1]) == month && Convert.ToInt32(s[2]) == weekNoOfMonth && w == week)
                {

                    return s[5];
                }

            }
            return "";

        }



        #endregion
    }
}
