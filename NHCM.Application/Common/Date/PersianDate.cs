using System;
using System.Linq;
using System.Globalization;


namespace PersianLibrary
{
    public class PersianDate
    {
        #region CTor

        public PersianDate()
        {
        }

        public PersianDate(string persianDate)
        {
            string[] chunks = persianDate.Trim().Split('/', '\\', '-', ':');

            if (chunks.Count() != 3 && chunks.Count() != 7)
                throw new Exception("Invalid PersianDate Format...");

            this.Hour = 0;
            this.Minute = 0;
            this.Second = 0;
            this.Millisecond = 0;

            this.Year = GetExceptionFromStringToInt(chunks[0]);
            this.Month = GetExceptionFromStringToInt(chunks[1]);
            this.Day = GetExceptionFromStringToInt(chunks[2]);

            if (chunks.Count() == 7)
            {
                this.Hour = GetExceptionFromStringToInt(chunks[3]);
                this.Minute = GetExceptionFromStringToInt(chunks[4]);
                this.Second = GetExceptionFromStringToInt(chunks[5]);
                this.Millisecond = GetExceptionFromStringToInt(chunks[6]);
            }
        }

        public PersianDate(DateTime dateTime)
        {
            this.FormalDateTime = dateTime;
        }

        public PersianDate(int year, int month, int day)
            : this(year, month, day, 0, 0, 0, 0)
        {

        }

        public PersianDate(int year, int month, int day, int hour, int minute, int second, int millisecond)
        {
            this.Year = year;
            this.Month = month;
            this.Day = day;
            this.Hour = hour;
            this.Minute = minute;
            this.Second = second;
            this.Millisecond = millisecond;
        }

        #endregion

        #region Fields

        private string[] weeks = new string[] { "يکشنبه", "دوشنبه", "سه شنبه", "چهارشنبه", "پنج شنبه", "یکشنبه", "شنبه" };

        private static string[] months = new string[] { "حمل", "ثور", "جوزا", "سرطان", "اسد", "سنبله", "ميزان", "عقرب", "قوس", "جدی", "دلو", "حوت" };

        private static string[] Gregorian_Months = new string[] { "جنوری", "فبروری", "مارچ", "اپریل", "می", "جون", "جولای", "آگست", "سپتمبر", "اکتوبر", "نومبر", "دسمبر" };

        private System.Globalization.PersianCalendar persianCalendar = new PersianCalendar();

        #endregion

        #region Properties

        public static PersianDate Now
        {
            get
            {
                return new PersianDate(DateTime.Now);
            }
        }

        /// <summary>
        /// <para> Determines whether the specified date in the Persian era is a leap day.</para>
        /// <para>In Persian: روزآخراسفند</para>
        /// </summary>    
        public bool IsLeapDay
        {
            get
            {
                return persianCalendar.IsLeapDay(this.Year, this.Month, this.Day);
            }
        }

        /// <summary>
        /// <para> Determines whether the specified year in the Persian era is a leap year.</para>
        /// <para>In Persian: کبیسه</para>
        /// </summary>        
        public bool IsLeapYear
        {
            get
            {
                return persianCalendar.IsLeapYear(this.Year);
            }
        }

        /// <summary>
        /// Gets or sets equivalent to Syste.DateTime of the Persian date.
        /// </summary> 
        private DateTime _formalDateTime;
        public DateTime FormalDateTime
        {
            get
            {
                return _formalDateTime;
            }
            set
            {
                _formalDateTime = value;

                if (_formalDateTime < persianCalendar.MinSupportedDateTime || _formalDateTime > persianCalendar.MaxSupportedDateTime)
                    throw new Exception("Date can not be less that 01-01-622");
                int year = persianCalendar.GetYear(_formalDateTime);
                int month = persianCalendar.GetMonth(_formalDateTime);
                int day = persianCalendar.GetDayOfMonth(_formalDateTime);
                if (this.Year != year || this.Month != month || this.Day != day)
                {
                    this.Year = year;
                    this.Month = month;
                    this.Day = day;
                }
            }
        }

        private string _dateString;
        /// <summary>
        /// <para>Gets the Persian date as a string.</para>
        /// <para>For example: 1392/01/02.</para>
        /// </summary> 
        public string DateString
        {
            get { return _dateString; }
            private set { _dateString = value; }
        }

        private string _monthString;
        /// <summary>
        /// <para>Gets the month of the Persian Date is a native string</para>
        /// <para>For example: اردیبهشت</para>
        /// </summary> 
        public string MonthString
        {
            get { return _monthString; }
            private set { _monthString = value; }
        }

        private string _dayOfWeek;
        /// <summary>
        /// <para>Gets the day of the week of the Persian Date is a native string</para>
        /// <para>For example: جمعه</para>
        /// </summary> 
        public string DayOfWeek
        {
            get { return _dayOfWeek; }
            private set { _dayOfWeek = value; }
        }

        private int _year;
        public int Year
        {
            get { return _year; }
            set
            {
                _year = value;
                SetDateTime();
            }
        }

        private int _month;
        public int Month
        {
            get { return _month; }
            set
            {
                _month = value;
                SetDateTime();
            }
        }

        private int _day;
        public int Day
        {
            get { return _day; }
            set
            {
                _day = value;
                SetDateTime();
            }
        }

        private int _hour;
        public int Hour
        {
            get { return _hour; }
            set
            {
                _hour = value;
                SetDateTime();
            }
        }

        private int _minute;
        public int Minute
        {
            get { return _minute; }
            set
            {
                _minute = value;
                SetDateTime();
            }
        }

        private int _second;
        public int Second
        {
            get { return _second; }
            set
            {
                _second = value;
                SetDateTime();
            }
        }

        private int _millisecond;
        public int Millisecond
        {
            get { return _millisecond; }
            set
            {
                _millisecond = value;
                SetDateTime();
            }
        }

        public static PersianDate MaxValue
        {
            get
            {
                return new PersianDate((new PersianCalendar()).MaxSupportedDateTime);
            }
        }

        public static PersianDate MinValue
        {
            get
            {
                return new PersianDate((new PersianCalendar()).MinSupportedDateTime);
            }
        }



        #endregion

        #region Methods

        #region Public


        /// <summary>
        /// <para>Returnes a PersianDate object that is offset the specified number of months away from the specified PersianDate</para>
        /// </summary> 
        public static string GetGregorianMonthInPersianString(int months)
        {
            return Gregorian_Months[months - 1];
        }

        public static string GetFormatedString(DateTime? Date)
        {

            // Changed for presentation
            if(Date != null)
            {
                PersianLibrary.PersianDate PD = PersianLibrary.PersianDate.Convert(Date.Value);
                String PDString = PD.DayOfWeek + "، " + PD.Day + " " + PD.MonthString + " " + PD.Year;
                return PDString;
            }
            else
            {
                return null;
            }

           
        }

        public static System.Collections.Generic.List<object> GetPersianMonths()
        {
            System.Collections.Generic.List<object> lst = new System.Collections.Generic.List<object>();
            for (int i = 0; i < months.Length; i++)
            {
                lst.Add(new { Value = i + 1, Text = months[i] });
            }
            return lst;
        }




        /// <summary>
        /// <para>Returnes a PersianDate that is specified number of days away from the specified PersianDate</para>
        /// </summary> 
        public PersianDate AddDays(int days)
        {
            return new PersianDate(persianCalendar.AddDays(FormalDateTime, days));
        }

        /// <summary>
        /// <para>Returnes a PersianDate object that is offset the specified number of months away from the specified PersianDate</para>
        /// </summary> 
        public PersianDate AddMonths(int months)
        {
            return new PersianDate(persianCalendar.AddMonths(FormalDateTime, months));
        }

        /// <summary>
        /// <para>Returnes a PersianDate object that is offset the specified number of years away from the specified PersianDate</para>
        /// </summary> 
        public PersianDate AddYears(int years)
        {
            return new PersianDate(persianCalendar.AddYears(FormalDateTime, years));
        }

        /// <summary>
        /// <para>Returnes a PersianDate that is specified number of hours away from the specified PersianDate</para>
        /// </summary> 
        public PersianDate AddHours(int hours)
        {
            return new PersianDate(persianCalendar.AddHours(FormalDateTime, hours));
        }

        /// <summary>
        /// <para>Returnes a PersianDate that is specified number of minutes away from the specified PersianDate</para>
        /// </summary> 
        public PersianDate AddMinutes(int minutes)
        {
            return new PersianDate(persianCalendar.AddMinutes(FormalDateTime, minutes));
        }

        /// <summary>
        /// <para>Returnes a PersianDate that is specified number of seconds away from the specified PersianDate</para>
        /// </summary> 
        public PersianDate AddSeconds(int seconds)
        {
            return new PersianDate(persianCalendar.AddSeconds(FormalDateTime, seconds));
        }

        /// <summary>
        /// <para>Returnes a PersianDate that is specified number of milliseconds away from the specified PersianDate</para>
        /// </summary> 
        public PersianDate AddMilliseconds(int milliseconds)
        {
            return new PersianDate(persianCalendar.AddMilliseconds(FormalDateTime, milliseconds));
        }

        /// <summary>
        /// <para>Returnes a PersianDate that is specified number of weeks away from the specified PersianDate</para>
        /// </summary> 
        public PersianDate AddWeeks(int weeks)
        {
            return new PersianDate(persianCalendar.AddWeeks(FormalDateTime, weeks));
        }

        /// <summary>
        /// <para>Returnes a PersianDate objectthat is a new instance of PersianDate with the same values</para>
        /// </summary> 
        public PersianDate Clone()
        {
            return new PersianDate(this.Year, this.Month, this.Day, this.Hour, this.Minute, this.Second, this.Millisecond);
        }

        /// <summary>
        /// Returnes a System.DateTime object equivalent to the Persian date
        /// </summary>
        /// <param name="Input">Persian year, month and day as int</param>

        public static DateTime ToDate(int year, int month, int day)
        {
            return ToDate(year, month, day, 0, 0, 0, 0);
        }
        /// <summary>
        /// Returnes a System.DateTime object equivalent to the Persian date
        /// </summary>
        /// <param name="Input">Persian year, month and day and times as int</param>
        public static DateTime ToDate(int year, int month, int day, int hour, int minute, int second, int millisecond)
        {
            PersianDate persianDate = new PersianDate(year, month, day, hour, minute, second, millisecond);
            return persianDate.FormalDateTime;
        }

        /// <summary>
        /// Compares two instances of PersianDate and returns an integer that indicates whether the first instance is earlier than, the same as, or later than the second instance.
        /// </summary>
        public static int Compare(PersianDate firstPersianDate, PersianDate secondPersianDate)
        {
            return DateTime.Compare(firstPersianDate.FormalDateTime, secondPersianDate.FormalDateTime);
        }

        /// <summary>
        /// Compares the value of this instance to a specified PersianDate value and returns an integer that indicates whether this instance is earlier than, the same as, or later than the specified PersianDate value.
        /// </summary>
        public int CompareTo(PersianDate persianDate)
        {
            return DateTime.Compare(this.FormalDateTime, persianDate.FormalDateTime);
        }



        /// <summary>
        /// <para>An Static method, returnes a PersianDate</para>
        /// <para>Converts a System.DateTime to PersianDate</para>
        /// </summary>
        public static PersianDate Convert(DateTime date)
        {
            return new PersianDate(date);
        }

        #endregion

        #region Private

        private int GetExceptionFromStringToInt(string number)
        {
            int convertedNumber = 0;

            if (!int.TryParse(number, out convertedNumber))
                throw new Exception("Can not convert string to PersianDate");

            return convertedNumber;
        }

        private void SetDateTime()
        {
            if (this.Day == 0 || this.Month == 0 || this.Year == 0)
                return;

            this.DateString = string.Format("{0}/{1}/{2}", this.Year.ToString().PadLeft(3, '0'), this.Month.ToString().PadLeft(2, '0'), this.Day.ToString().PadLeft(2, '0'));

            this.FormalDateTime = persianCalendar.ToDateTime(this.Year, this.Month, this.Day, this.Hour, this.Minute, this.Second, this.Millisecond);

            this.MonthString = months[this.Month - 1];

            this.DayOfWeek = weeks[(int)(this.FormalDateTime.DayOfWeek)];
        }

        #endregion


        #endregion
    }
}
