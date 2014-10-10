namespace LOGI.Framework.Toolkit.Core.Reflection
{
    ///<summary>
    /// Property Reflection Methods
    ///</summary>
    public static class Property
    {
        /// <summary>
        /// TObj tipindeki objenin, propName isimli propertisine TValue tipinde değer atar
        /// </summary>
        /// <typeparam name="TObj">Properti değeri değişecek sınıf objesi tipi</typeparam>
        /// <typeparam name="TValue">Properti değeri değişecek propertinini alacağı değer tipi</typeparam>
        /// <param name="soruceObj">Properti değeri değişecek sınıf objesi</param>
        /// <param name="propName">Değeri değişecek properti adı</param>
        /// <param name="propValue">Değeri değişecek, TValue tipinde properti değeri</param>
        /// <returns></returns>
        public static void SetValue<TObj, TValue>(TObj soruceObj, TValue propValue, string propName)
        {
            var type = typeof(TObj);
            var pi = type.GetProperty(propName);
            if (pi != null)
            {
                pi.SetValue(soruceObj, propValue, null);
            }
        }
    }
}
