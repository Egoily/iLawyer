using System;
using System.Collections;
using System.Globalization;

namespace ee.iLawyer.Domain
{
    public class BLDateHelper
    {
        #region 年份，返回ArrayList类型
        public static ArrayList Year()
        {
            ArrayList arraylist = new ArrayList(210);
            for (int i = 1901; i < 2101; i++)
            {

                arraylist.Add(i);
            }
            return arraylist;
        }
        #endregion

        #region 公历月份，返回ArrayList类型
        public static ArrayList Month()
        {
            ArrayList arraylist = new ArrayList();
            for (int i = 1; i <= 12; i++)
            {
                arraylist.Add(i);
            }
            return arraylist;
        }
        #endregion

        #region 根据年月返回公历当月的天数
        public static int GetMonthCount(int year, int month)
        {
            int count = DateTime.DaysInMonth(year, month);
            return count;
        }
        #endregion

        #region 根据年月返回当年当月第一天星期几
        //2000-1-2为周日
        //返回以0为周日
        public static int FirstDayOfWeekOfMonth(int year, int month)
        {
            DateTime jztime = new DateTime(2000, 1, 2);
            DateTime newtime = new DateTime(year, month, 1);
            TimeSpan ts;
            int dayofweek = 0;
            if (jztime < newtime)
            {
                ts = newtime - jztime;
                dayofweek = ts.Days % 7;
                return dayofweek;
            }
            else
            {
                ts = jztime - newtime;
                dayofweek = (ts.Days) % 7;
                return (7 - dayofweek) % 7;
            }
        }
        #endregion

        #region 根据年月日算星期几,返回string类型,方法二：(简捷)以一个基准日期，两日期相减算法
        //方法三
        //以一个基准日期，两日期相减算法
        //根据年月日算星期几,返回string类型
        //2000-1-1为星期六
        public static string GetWeekOfDay(int year, int month, int day)
        {
            //int runnian=0;
            DateTime jztime = new DateTime(2000, 1, 1);
            DateTime newtime = new DateTime(year, month, day);
            TimeSpan chatime;
            int week;
            string weekstr = "";
            if (jztime <= newtime)
            {
                chatime = newtime - jztime;
                week = (chatime.Days + 1) % 7;
                switch (week)
                {
                    case 1:
                        weekstr = "星期六";
                        break;
                    case 2:
                        weekstr = "星期日";
                        break;
                    case 3:
                        weekstr = "星期一";
                        break;
                    case 4:
                        weekstr = "星期二";
                        break;
                    case 5:
                        weekstr = "星期三";
                        break;
                    case 6:
                        weekstr = "星期四";
                        break;
                    case 0:
                        weekstr = "星期五";
                        break;
                }
            }
            else
            {
                chatime = jztime - newtime;
                week = (chatime.Days + 1) % 7;
                switch (week)
                {
                    case 1:
                        weekstr = "星期六";
                        break;
                    case 2:
                        weekstr = "星期五";
                        break;
                    case 3:
                        weekstr = "星期四";
                        break;
                    case 4:
                        weekstr = "星期三";
                        break;
                    case 5:
                        weekstr = "星期二";
                        break;
                    case 6:
                        weekstr = "星期一";
                        break;
                    case 0:
                        weekstr = "星期日";
                        break;
                }
            }
            return weekstr;

        }
        #endregion

        #region 根据公历日期返回农历日
        public static string GetNLMonth(int year, int month, int day)
        {
            string[] monthname = { "正月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "腊月" };
            var calendar = new ChineseLunisolarCalendar();
            DateTime datetime = new DateTime(year, month, day);

            int nlyear = calendar.GetYear(datetime);
            int nlmonth = calendar.GetMonth(datetime);

            //判断是否有闰月 ,leap=0即没有闰月
            int leapmonth = calendar.GetLeapMonth(nlyear);
            if (leapmonth > 0)
            {
                if (leapmonth <= nlmonth)
                {
                    nlmonth--;
                }
            }
            return monthname[nlmonth - 1].ToString();
        }
        #endregion

        #region 根据公历日期返回农历日
        public static string GetNLDay(int year, int month, int day)
        {
            string[] nstr1 = { "一", "二", "三", "四", "五", "六", "七", "八", "九", "十" };
            string[] nstr2 = { "初", "十", "廿", "卅" };
            string[] monthname = { "正月", "二月", "三月", "四月", "五月", "六月", "七月", "八月", "九月", "十月", "十一月", "腊月" };
            var calendar = new ChineseLunisolarCalendar();
            DateTime datetime = new DateTime(year, month, day);

            int nlyear = calendar.GetYear(datetime);
            int nlmonth = calendar.GetMonth(datetime);
            int nlday = calendar.GetDayOfMonth(datetime);

            //判断是否有闰月 ,leap=0即没有闰月
            int leapmonth = calendar.GetLeapMonth(nlyear);
            if (leapmonth > 0)
            {
                if (leapmonth <= nlmonth)
                {
                    nlmonth--;
                }
            }
            if (nlday == 1)
            {
                return monthname[nlmonth - 1].ToString();
            }
            else
            {
                if (nlday > 1 && nlday <= 10)
                {
                    return nstr2[0].ToString() + nstr1[nlday - 1].ToString();
                }
                else if (nlday > 10 && nlday < 20)
                {
                    return nstr2[1].ToString() + nstr1[nlday % 10 - 1].ToString();
                }
                else if (nlday == 20)
                {
                    return "二十";
                }
                else if (nlday > 20 && nlday < 30)
                {
                    return nstr2[2].ToString() + nstr1[nlday % 10 - 1].ToString();
                }
                else if (nlday == 30)
                {
                    return "三十";
                }
                else
                {
                    return "卅一";
                }
            }

        }
        #endregion

        #region 阳历转化为农历，返回string类型
        //ChineseLunisolarCalendar类能实现的日期为1901-2-19——2101-1-28
        //阳历转化为农历，返回string类型
        //以公元前2997年为中国历史上第一个甲子年
        public static string GetNLYear(int year, int month, int day)
        {
            string datestr = "";
            string[] gan = { "甲", "乙", "丙", "丁", "戊", "己", "庚", "辛", "壬", "癸" };
            string[] zhi = { "子", "丑", "寅", "卯", "辰", "巳", "午", "未", "申", "酉", "戌", "亥" };
            string[] shengxiao = { "鼠", "牛", "虎", "免", "龙", "蛇", "马", "羊", "猴", "鸡", "狗", "猪" };
            var calendar = new ChineseLunisolarCalendar();
            DateTime date = new DateTime(year, month, day);

            int nlyear = calendar.GetYear(date);
            int nlmonth = calendar.GetMonth(date);
            int nlday = calendar.GetDayOfMonth(date);

            //判断是否有闰月 ,leap=0即没有闰月
            int leapmonth = calendar.GetLeapMonth(nlyear);
            if (leapmonth > 0)
            {
                if (leapmonth <= nlmonth)
                {
                    nlmonth--;
                }
            }

            //确定年份,基准1804为甲子年,鼠年
            //天干年
            string zy = "";
            //地支年
            string dy = "";
            //生肖年
            string sxnian = "";
            if (nlyear > 3)
            {
                int zhiyear = (nlyear - 4) % 10;
                int diyear = (nlyear - 4) % 12;
                zy = gan[zhiyear];
                dy = zhi[diyear];
                sxnian = shengxiao[diyear];
            }

            //转化的最终农历年
            datestr = zy + dy + "(" + nlyear + ")" + "年" + "  生肖:" + sxnian;

            return datestr;
        }
        #endregion


        public static string GetNL(int year, int month, int day)
        {
            string nlDay = GetNLDay(year, month, day);
            string nlMonth = GetNLMonth(year, month, day);
            string nlYear = GetNLYear(year, month, day);
            return nlYear + " " + nlMonth + " " + nlDay;
        }
    }
}
