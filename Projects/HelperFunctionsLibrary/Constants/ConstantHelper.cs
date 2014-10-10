using System;
using System.Collections;
using System.Configuration;
using System.Web;

namespace LOGI.Framework.Toolkit.HelperLibrary.Constants
{
    public abstract class ConstantHelperBase
    {
        /// <summary>
        /// AppConfig'den ilgili sub key yoksa site üzerinden alır
        /// </summary>
        /// <param name="siteAppSettingValue"></param>
        /// <param name="subAppSettingKey"></param>
        /// <returns></returns>
        public string GetAppConfigValueFromSiteIfEmpty(string siteAppSettingValue, string subAppSettingKey)
        {
            var appSettingValue = ConfigurationManager.AppSettings[subAppSettingKey];

            var retValue =
                    !string.IsNullOrEmpty(appSettingValue)
                        ? appSettingValue
                        : siteAppSettingValue;

            return retValue;
        }

    }

    ///<summary>
    /// static property değerlerine erişimi sağlayan sınıf
    ///</summary>
    public sealed class ConstantHelper : ConstantHelperBase
    {
        private const string Log2DbCategoryNameKey = "Log2DbCategoryName";
        private const string EnableValidationKey = "EnableValidations";
        private const string SiteInTestMode = "SiteInTestMode";
        private const string EnableLogKey = "EnableLog";

        ///<summary>
        /// Web uygulaması işlemlerinin static property değerlerine erişimi sağlayan sınıf
        ///</summary>
        public sealed class Web
        {
            ///<summary>
            /// BasketTripItemRefNoPrefix
            ///</summary>
            public static readonly string ToTitleCaseRegexFormat = "ToTitleCaseFormat";

            ///<summary>
            /// BasketTripItemRefNoPrefix
            ///</summary>
            public static readonly string BasketTripItemRefNoPrefix = GetBasketTripItemRefNoPrefix();

            private static string GetBasketTripItemRefNoPrefix()
            {
                var returnValue = "GUH-";

                var configValue = ConfigurationManager.AppSettings["BasketRefNoPrefix"];

                if (!string.IsNullOrEmpty(configValue))
                {
                    returnValue = configValue;
                }

                return returnValue;
            }

            ///<summary>
            /// GeneratingUniqueMinSize
            ///</summary>
            public static readonly int GeneratingUniqueMinSize = 8;

            ///<summary>
            /// GeneratingUniqueMaxSize
            ///</summary>
            public static readonly int GeneratingUniqueMaxSize = 10;

            ///<summary>
            /// ApplicationName
            ///</summary>
            public static readonly string ApplicationName = ConfigurationManager.AppSettings["ApplicationName"];

            ///<summary>
            /// AffiliateUser
            ///</summary>
            public static readonly string AffiliateCode = ConfigurationManager.AppSettings["AffiliateCode"];

            ///<summary>
            /// AffiliateUser
            ///</summary>
            public static readonly string AffiliateUser = ConfigurationManager.AppSettings["AffiliateUser"];

            ///<summary>
            /// AffiliatePassword
            ///</summary>
            public static readonly string AffiliatePassword = ConfigurationManager.AppSettings["AffiliatePassword"];

            ///<summary>
            /// AffiliateIPAdrress
            ///</summary>
            public static readonly string AffiliateIPAddress = ConfigurationManager.AppSettings["AffiliateIPAddress"];

            ///<summary>
            /// TravelPortalConnectionString
            ///</summary>
            public static readonly string TravelPortalConnectionString = ConfigurationManager.ConnectionStrings["TravelPortal"] != null ? ConfigurationManager.ConnectionStrings["TravelPortal"].ConnectionString : string.Empty;

            ///<summary>
            ///Decimal değerlerin ToString ile hassasiyet'in kaç karakter olacağını belirler.
            ///</summary>
            public static readonly string SiteCurrencyToStringFormat = ConfigurationManager.AppSettings["SiteCurrencyToStringFormat"];

            ///<summary>
            /// DeveloperMode
            ///</summary>
            public static readonly string SiteCulture = ConfigurationManager.AppSettings["SiteCulture"];

            ///<summary>
            /// SiteCurrencySymbol
            ///</summary>
            public static readonly string SiteDefaultCurrencySymbol = ConfigurationManager.AppSettings["SiteDefaultCurrencySymbol"];

            ///<summary>
            /// DeveloperMode
            ///</summary>
            public static readonly string DeveloperMode = ConfigurationManager.AppSettings["DeveloperMode"];

            //public static readonly string ScriptVersionNo = ConfigurationManager.AppSettings["ScriptVersionNo"] ?? string.Empty;
            //public static readonly string CssVersionNo = ConfigurationManager.AppSettings["CssVersionNo"] ?? string.Empty;
            //public readonly static string ImagePrefix = ConfigurationManager.AppSettings["ImgPrefix"] ?? string.Empty;
            //public readonly static string ScriptPrefix = ConfigurationManager.AppSettings["JsPrefix"] ?? string.Empty;
            //public readonly static string CssPrefix = ConfigurationManager.AppSettings["CssPrefix"] ?? string.Empty;
            //public readonly static string CommonCssFileName = System.Configuration.ConfigurationManager.AppSettings["CommonCssSet"] ?? string.Empty;

            ///<summary>
            /// ActivationRequired
            ///</summary>
            public readonly static string ActivationRequired = ConfigurationManager.AppSettings["ActivationRequired"];

            ///<summary>
            /// DisableDOSCheck
            ///</summary>
            public static readonly string DisableDosCheck = ConfigurationManager.AppSettings["DisableDOSCheck"];

            //public static readonly string AdminEmail = ConfigurationManager.AppSettings["AdminEmail"] ?? string.Empty;

            ///<summary>
            /// SiteHost
            ///</summary>
            public static readonly string SiteHost = ConfigurationManager.AppSettings["SiteHost"];

            ///<summary>
            /// DisableCache
            ///</summary>
            public static readonly string DisableCache = ConfigurationManager.AppSettings["DisableCache"];

            ///<summary>
            /// Service Request Time Limit As Second
            ///</summary>
            public static readonly string RequestTimeLimitAsSecond = ConfigurationManager.AppSettings["SiteRequestTimeLimitAsSecond"];

            ///<summary>
            /// Service Book Request Time Limit As Second
            ///</summary>
            public static readonly string BookRequestTimeLimitAsSecond = ConfigurationManager.AppSettings["SiteBookRequestTimeLimitAsSecond"];

            ///<summary>
            /// SiteDefaultMailFrom
            ///</summary>
            public static readonly string DefaultMailFrom = ConfigurationManager.AppSettings["SiteDefaultMailFrom"];

            ///<summary>
            /// SiteDefaultMailToList
            ///</summary>
            public static readonly string DefaultMailTo = ConfigurationManager.AppSettings["SiteDefaultMailToList"];

            ///<summary>
            /// MailGroupAgency_ForExceptions
            ///</summary>
            public static readonly string MailGroupAgency_ForExceptions = ConfigurationManager.AppSettings["MailGroupAgency_ForExceptions"];

            ///<summary>
            /// Rezervasyon onay işleminin aktif olma durumu
            ///</summary>
            public static bool EnableReservationApproval
            {
                get
                {
                    return GetBooleanValue("EnableReservationApproval");
                }
            }

            #region BasketTransactionResult

            /// <summary>
            /// Basket Trip Item TransactionResult görünümü
            /// </summary>
            ///<returns></returns>
            public static string GetBasketTransactionResultTripItemTemplateFile()
            {
                var retValue = ConfigurationManager.AppSettings["BasketTransactionResultTripItemTemplateFile"];



                return retValue;
            }

            /// <summary>
            /// Basket Trip Success Item TransactionResult görünümü
            /// </summary>
            ///<returns></returns>
            public static string GetBasketTransactionSuccessGroupsResultTemplateFile()
            {
                var retValue = ConfigurationManager.AppSettings["BasketTransactionSuccessGroupsResultTemplateFile"];



                return retValue;
            }

            /// <summary>
            /// Basket Trip Failed Item TransactionResult görünümü
            /// </summary>
            ///<returns></returns>
            public static string GetBasketTransactionFailedGroupsResultTemplateFile()
            {
                var retValue = ConfigurationManager.AppSettings["BasketTransactionFailedGroupsResultTemplateFile"];



                return retValue;
            }

            /// <summary>
            /// Basket Trip Cancelled Item TransactionResult görünümü
            /// </summary>
            ///<returns></returns>
            public static string GetBasketTransactionCancelledGroupsResultTemplateFile()
            {
                var retValue = ConfigurationManager.AppSettings["BasketTransactionCancelledGroupsResultTemplateFile"];



                return retValue;
            }
            #endregion

            #region ShoppingDetailTransactionResult

            /// <summary>
            /// Alışveriş detayı görünümü
            /// </summary>
            ///<returns></returns>
            public static string GetShoppingDetailTemplateFile()
            {
                var retValue = ConfigurationManager.AppSettings["ShoppingDetailTemplateFile"];



                return retValue;
            }


            /// <summary>
            ///Alışveriş detayı görünümü
            /// </summary>
            ///<returns></returns>
            public static string GetShoppingDetailTripItemTemplateFile()
            {
                var retValue = ConfigurationManager.AppSettings["ShoppingDetailTripItemTemplateFile"];



                return retValue;
            }

            /// <summary>
            ///My Trip ınfo
            /// </summary>
            ///<returns></returns>
            public static string GetMyTripInfoTripItemTemplateFile()
            {
                var retValue = ConfigurationManager.AppSettings["MyTripInfoTripItemTemplateFile"];



                return retValue;
            }
            #endregion

            #region BasketTemplateFile
            /// <summary>
            /// Basket için container folder düzenlemesi
            /// </summary>
            /// <param name="filePath">Template file path</param>
            /// <returns>Container Folder Path + Template File Path</returns>
            internal static string getBasketTemplateFileFullPath(IDictionary tokens, string filePath)
            {
                var containerFolderKey = ConstantHelper.Web.BasketContainerFolderKey;

                if (tokens != null && tokens.Contains(containerFolderKey))
                {
                    containerFolderKey = tokens[containerFolderKey].ToString();
                }

                var rootValue = ConfigurationManager.AppSettings[containerFolderKey];

                if (string.IsNullOrEmpty(rootValue))
                {
                    rootValue = ConfigurationManager.AppSettings[ConstantHelper.Web.BasketContainerFolderKey];
                }

                if (!rootValue.EndsWith("/"))
                {
                    rootValue += "/";
                }

                var returnValue = (rootValue + filePath).Replace("//", "/");

                return returnValue;
            }
            /// <summary>
            /// Basket görünümü
            /// </summary>
            ///<returns></returns>
            public static string GetBasketTemplateFile(IDictionary tokens)
            {
                var retValue = ConfigurationManager.AppSettings["BasketTemplateFile"];

                retValue = ConstantHelper.Web.getBasketTemplateFileFullPath(tokens, retValue);

                return retValue;
            }

            /// <summary>
            /// Basket Trip Item görünümü
            /// </summary>
            ///<returns></returns>
            public static string GetBasketTripItemTemplateFile(IDictionary tokens)
            {
                var retValue = ConfigurationManager.AppSettings["BasketTripItemTemplateFile"];

                retValue = ConstantHelper.Web.getBasketTemplateFileFullPath(tokens, retValue);

                return retValue;
            }

            /// <summary>
            /// Basket Trip Passenger Item görünümü (trip içindeki herbir passenger için)
            /// </summary>
            ///<returns></returns>
            public static string GetBasketTripPassengerItemTemplateFile(IDictionary tokens)
            {
                var retValue = ConfigurationManager.AppSettings["BasketTripPassengerItemTemplateFile"];

                return retValue;
            }

            /// <summary>
            /// Basket Trip Item PassengerInfo görünümü (trip içindeki tüm passengerlar için)
            /// </summary>
            ///<returns></returns>
            public static string GetBasketTripItemPassengerInfoTemplateFile()
            {
                var retValue = ConfigurationManager.AppSettings["BasketTripItemPassengerInfoTemplateFile"];



                return retValue;
            }

            /// <summary>
            /// Basket Trip Item Options görünümü
            /// </summary>
            ///<returns></returns>
            public static string GetBasketTripOptionItemTemplateFile(IDictionary tokens)
            {
                var retValue = ConfigurationManager.AppSettings["BasketTripOptionItemTemplateFile"];

                retValue = ConstantHelper.Web.getBasketTemplateFileFullPath(tokens, retValue);

                return retValue;
            }


            /// <summary>
            /// Basket Trip Miles Card görünümü
            /// </summary>
            ///<returns></returns>
            public static string GetBasketTripMilesCardWrapperTemplateFile()
            {
                var retValue = ConfigurationManager.AppSettings["BasketTripMilesCardWrapperTemplateFile"];



                return retValue;
            }

            /// <summary>
            /// Basket Trip SpecialRequest görünümü
            /// </summary>
            ///<returns></returns>
            public static string GetBasketTripSpecialRequestWrapperTemplateFile()
            {
                var retValue = ConfigurationManager.AppSettings["BasketTripSpecialRequestWrapperTemplateFile"];



                return retValue;
            }

            /// <summary>
            /// Basket Trip Item Rule görünümü
            /// </summary>
            ///<returns></returns>
            public static string GetBasketRuleTemplateFile(IDictionary tokens)
            {
                var retValue = ConfigurationManager.AppSettings["BasketRuleTemplateFile"];

                retValue = ConstantHelper.Web.getBasketTemplateFileFullPath(tokens, retValue);

                return retValue;
            }

            /// <summary>
            /// Basket Campaign Rule görünümü
            /// </summary>
            ///<returns></returns>
            public static string GetBasketCampaignTemplateFile(IDictionary tokens)
            {
                var retValue = ConfigurationManager.AppSettings["BasketCampaignTemplateFile"];

                retValue = ConstantHelper.Web.getBasketTemplateFileFullPath(tokens, retValue);

                return retValue;
            }

            /// <summary>
            /// Basket TransactionResult görünümü
            /// </summary>
            ///<returns></returns>
            public static string GetBasketTransactionResultTemplateFile()
            {
                var retValue = ConfigurationManager.AppSettings["BasketTransactionResultTemplateFile"];



                return retValue;
            }

            /// <summary>
            /// Basket Trip Item Rule görünümü cache key'i
            /// </summary>
            public static readonly string BasketTripItemRuleViewCacheKey = "LOGI.Framework.TravelPortal.UI.BusinessObjects.Shopping.GetItemRulesView";

            /// <summary>
            /// Basket Trip Item görünümü  cache key'i
            /// </summary>
            public static readonly string BasketTripItemDetailViewCacheKey = "LOGI.Framework.TravelPortal.UI.BusinessObjects.Shopping.GetDetailView";

            /// <summary>
            /// Basket Trip Item Option görünümü cache key'i
            /// </summary>
            public static readonly string BasketTripItemOptionViewCacheKey = "LOGI.Framework.TravelPortal.UI.BusinessObjects.Shopping.GetOptionItemsView";

            /// <summary>
            /// Basket Trip Item Option Container key'i
            /// </summary>
            public static readonly string BasketTripItemOptionContainerKey = "TripItemOptions";

            ///<summary>
            /// Basket template üst folder
            ///</summary>
            public static string BasketContainerFolderKey
            {
                get
                {
                    return "BasketContainerFolder";
                }
            }
            #endregion

            ///<summary>
            /// Test/prod durumunu belirtir
            ///</summary>
            public static bool ServiceModeInTest
            {
                get
                {
                    return GetBooleanValue("SiteServiceMode");
                }
            }

            ///<summary>
            /// Uygulama seviyesinde rezervasyonun yapılıp yapılamayacağı
            ///</summary>
            public static bool EnableReservation
            {
                get
                {
                    return GetBooleanValue("SiteEnableReservation", true);
                }
            }

            ///<summary>
            /// Uygulama seviyesinde fee alınıp alınmayacağı
            ///</summary>
            public static bool EnableServiceFee
            {
                get
                {
                    return GetBooleanValue("SiteEnableServiceFee", true);
                }
            }

            ///<summary>
            /// EnableFareBasis
            ///</summary>
            public static bool EnableFareBasis
            {
                get
                {
                    return GetBooleanValue("SiteEnableFareBasis", false);
                }
            }

            /// <summary>
            /// Mail template bilgileri
            /// </summary>
            public sealed class OrderMail
            {
                ///<summary>
                /// OrderResultTemplateFile
                ///</summary>
                public static string GetOrderResultTemplateFile()
                {
                    var retValue = ConfigurationManager.AppSettings["OrderResultTemplateFile"];



                    return retValue;
                }

                ///<summary>
                /// Basket'ın mail template görüntüsünü sağlayan view
                ///</summary>
                public static string GetBasketOrderMailTemplateFile()
                {
                    var retValue = ConfigurationManager.AppSettings["BasketOrderMailTemplateFile"];
                    return retValue;
                }

                ///<summary>
                /// Tüm Trip ve altındaki item'ların viewlerini içeren template
                ///</summary>
                public static string GetBasketOrderMailTripItemTemplateFile()
                {
                    var retValue = ConfigurationManager.AppSettings["BasketOrderMailTripItemTemplateFile"];



                    return retValue;
                }

                ///<summary>
                /// Order işlemlerinde gönderilecek mail içeriğini belirtilen tipe göre gösterecek ana view
                ///</summary>
                ///<param name="moduleName">Module Name</param>
                ///<returns>string</returns>
                public static string GetOrderMailTripItemTemplateFileByType(string moduleName)
                {
                    /*
                     <add key="ModuleTripItemOrderMailTemplateFile" value="Templates/Mail/Order/TripItem.{ModuleName}.cshtml"/>
             
                     */

                    var retValue = ConfigurationManager.AppSettings["ModuleTripItemOrderMailTemplateFile"];

                    if (!string.IsNullOrEmpty(retValue))
                    {
                        retValue = retValue.Replace("{ModuleName}", moduleName);
                    }

                    return retValue;
                }

                ///<summary>
                /// Order işlemlerinde gönderilecek mail içeriğini belirtilen tipe göre yolcuları gösterecek view
                ///</summary>
                ///<param name="moduleName">Module Name</param>
                ///<returns>string</returns>
                public static string GetTripItemOrderMailPassengerTemplateFileByType(string moduleName)
                {
                    /*
                    <add key="ModuleTripItemOrderMailPassengerTemplateFile" value="Templates/Mail/Order/TripItem.{ModuleName}.PassengerList.cshtml"/>
             
                    */

                    var retValue = ConfigurationManager.AppSettings["ModuleTripItemOrderMailPassengerTemplateFile"];

                    if (!string.IsNullOrEmpty(retValue))
                    {
                        retValue = retValue.Replace("{ModuleName}", moduleName);
                    }

                    return retValue;
                }

                ///<summary>
                /// Order işlemlerinde gönderilecek mail içeriğini belirtilen tipin item'larına göre gösterecek item view
                ///</summary>
                ///<param name="moduleName"></param>
                ///<returns></returns>
                public static string GetOrderMailItemTemplateFileByType(string moduleName)
                {
                    /*
                    <add key="ModuleItemOrderMailTemplateFile" value="Templates/Mail/Order/Item.{ModuleName}.cshtml"/>
             
                    */

                    var retValue = ConfigurationManager.AppSettings["ModuleItemOrderMailTemplateFile"];

                    if (!string.IsNullOrEmpty(retValue))
                    {
                        retValue = retValue.Replace("{ModuleName}", moduleName);
                    }

                    return retValue;
                }
            }

            /// <summary>
            /// Mail template bilgileri
            /// </summary>
            public sealed class FollowUp
            {
                ///<summary>
                /// OrderResultTemplateFile
                ///</summary>
                public static string GetOrderResultTemplateFile()
                {
                    var retValue = ConfigurationManager.AppSettings["OrderResultTemplateFile"];



                    return retValue;
                }

                ///<summary>
                /// Basket'ın mail template görüntüsünü sağlayan view
                ///</summary>
                public static string GetFollowUpTemplateFile()
                {
                    var retValue = ConfigurationManager.AppSettings["BasketFollowUpTemplateFile"];
                    return retValue;
                }

                ///<summary>
                /// Tüm Trip ve altındaki item'ların viewlerini içeren template
                ///</summary>
                public static string GetBasketFollowUpTripItemTemplateFile()
                {
                    var retValue = ConfigurationManager.AppSettings["BasketTripItemFollowUpTemplateFile"];



                    return retValue;
                }

                ///<summary>
                /// Order işlemlerinde gönderilecek mail içeriğini belirtilen tipe göre gösterecek ana view
                ///</summary>
                ///<param name="moduleName">Module Name</param>
                ///<returns>string</returns>
                public static string GetFollowUpTripItemTemplateFileByType(string moduleName)
                {
                    /*
                    <add key="ModuleTripItemFollowUpTemplateFile" value="Templates/FollowUp/TripItem.{ModuleName}.cshtml"/>
             
                    */

                    var retValue = ConfigurationManager.AppSettings["ModuleTripItemFollowUpTemplateFile"];

                    if (!string.IsNullOrEmpty(retValue))
                    {
                        retValue = retValue.Replace("{ModuleName}", moduleName);
                    }

                    return retValue;
                }

                ///<summary>
                /// Order işlemlerinde gönderilecek mail içeriğini belirtilen tipe göre yolcuları gösterecek view
                ///</summary>
                ///<param name="type">Module Name</param>
                ///<returns>string</returns>
                public static string GetTripItemFollowUpPassengerTemplateFileByType(string type)
                {
                    var retValue = ConfigurationManager.AppSettings[type + "TripItemPassengerFollowUpTemplateFile"];

                    return retValue;
                }

                ///<summary>
                /// Order işlemlerinde gönderilecek mail içeriğini belirtilen tipin item'larına göre gösterecek item view
                ///</summary>
                ///<param name="type"></param>
                ///<returns></returns>
                public static string GetFollowUpItemTemplateFileByType(string type)
                {
                    var retValue = ConfigurationManager.AppSettings[type + "ItemFollowUpTemplateFile"];

                    return retValue;
                }
            }

            /// <summary>
            /// Rezervasyon Mail template bilgileri
            /// </summary>
            public sealed class ReservationMail
            {
                ///<summary>
                /// ReservationResultTemplateFile
                ///</summary>
                public static string GetReservationResultTemplateFile()
                {
                    var retValue = ConfigurationManager.AppSettings["ReservationResultTemplateFile"];



                    return retValue;
                }

                ///<summary>
                /// Basket'ın mail template görüntüsünü sağlayan view
                ///</summary>
                public static string GetBasketReservationMailTemplateFile()
                {
                    var retValue = ConfigurationManager.AppSettings["BasketReservationMailTemplateFile"];



                    return retValue;
                }

                ///<summary>
                /// Tüm Trip ve altındaki item'ların viewlerini içeren template
                ///</summary>
                public static string GetBasketReservationMailTripItemTemplateFile()
                {
                    var retValue = ConfigurationManager.AppSettings["BasketReservationMailTripItemTemplateFile"];



                    return retValue;
                }

                ///<summary>
                /// Reservation işlemlerinde gönderilecek mail içeriğini belirtilen tipe göre gösterecek ana view
                ///</summary>
                ///<param name="moduleName">Module Name</param>
                ///<returns>string</returns>
                public static string GetReservationMailTripItemTemplateFileByType(string moduleName)
                {
                    /*
                    <add key="ModuleTripItemReservationMailTemplateFile" value="Templates/Mail/Reservation/TripItem.{ModuleName}.cshtml"/>
             
                    */

                    var retValue = ConfigurationManager.AppSettings["ModuleTripItemReservationMailTemplateFile"];

                    if (!string.IsNullOrEmpty(retValue))
                    {
                        retValue = retValue.Replace("{ModuleName}", moduleName);
                    }

                    return retValue;
                }

                ///<summary>
                /// Reservation işlemlerinde gönderilecek mail içeriğini belirtilen tipe göre yolcuları gösterecek view
                ///</summary>
                ///<param name="moduleName">Module Name</param>
                ///<returns>string</returns>
                public static string GetTripItemReservationMailPassengerTemplateFileByType(string moduleName)
                {
                    /*
                    <add key="ModuleTripItemReservationMailPassengerTemplateFile" value="Templates/Mail/Reservation/TripItem.{ModuleName}.PassengerList.cshtml"/>
             
                    */

                    var retValue = ConfigurationManager.AppSettings["ModuleTripItemReservationMailPassengerTemplateFile"];

                    if (!string.IsNullOrEmpty(retValue))
                    {
                        retValue = retValue.Replace("{ModuleName}", moduleName);
                    }

                    return retValue;
                }

                ///<summary>
                /// Reservation işlemlerinde gönderilecek mail içeriğini belirtilen tipin item'larına göre gösterecek item view
                ///</summary>
                ///<param name="moduleName"></param>
                ///<returns></returns>
                public static string GetReservationMailItemTemplateFileByType(string moduleName)
                {
                    /*
                    <add key="ModuleItemReservationMailTemplateFile" value="Templates/Mail/Reservation/Item.{ModuleName}.cshtml"/>
             
                    */

                    var retValue = ConfigurationManager.AppSettings["ModuleItemReservationMailTemplateFile"];

                    if (!string.IsNullOrEmpty(retValue))
                    {
                        retValue = retValue.Replace("{ModuleName}", moduleName);
                    }

                    return retValue;
                }
            }
        }

        ///<summary>
        /// BootStrapper işlemlerinin static property değerlerine erişimi sağlayan sınıf
        ///</summary>
        public sealed class BootStrapper
        {
            ///<summary>
            /// UnitySectionName
            ///</summary>
            public static readonly string UnitySectionName = "unity";

            ///<summary>
            /// DefaultServiceLocator, Toolkit.Core içerisinde tanımlı
            ///</summary>
            public static readonly string UnityContainerName = "YKM";

            ///<summary>
            /// DefaultServiceLocator, Toolkit.Core içerisinde tanımlı
            ///</summary>
            public static readonly string AssembliesFolder = ConfigurationManager.AppSettings["AssembliesFolder"];

            ///<summary>
            /// DefaultServiceLocator, Toolkit.Core içerisinde tanımlı
            ///</summary>
            public static readonly string DefaultServiceLocator = ConfigurationManager.AppSettings["DefaultServiceLocator"];

            ///<summary>
            /// UnityConfigurationPath
            ///</summary>
            public static readonly string UnityConfigurationPath = ConfigurationManager.AppSettings["UnityConfigurationPath"];

            ///<summary>
            /// JSON Serializer
            ///</summary>
            public static readonly string JsonSerializer = "JSON";

            ///<summary>
            /// XML Serializer
            ///</summary>
            public static readonly string XmlSerializer = "XML";

            ///<summary>
            /// Sharp Serializer
            ///</summary>
            public static readonly string CoreSharperSerializer = "Sharper";

            ///<summary>
            /// IMKB Döviz kur servisi
            ///</summary>
            public static readonly string IMKBExchangeRate = "IMKB";

            ///<summary>
            /// DB'den Döviz kur servisi
            ///</summary>
            public static readonly string FromDBExchangeRate = "Database";

            ///<summary>
            /// DB'den Döviz kur servisi
            ///</summary>
            public static readonly string FromAmadeusExchangeRate = "Amadeus";

            ///<summary>
            /// DataContract Serializer
            ///</summary>
            public static readonly string DataContractSerializer = "DataContractSerializer";

            ///<summary>
            /// SendMessage2Db
            ///</summary>
            public static readonly string SendMessage2Db = "SendMessage2Db";

            ///<summary>
            ///</summary>
            public static readonly string SendSms2Db = "SendSms2Db";
        }

        ///<summary>
        ///</summary>
        public sealed class InsuranceProviders
        {
            #region Log2
            ///<summary>
            /// Log2CategoryName
            ///</summary>
            public static readonly string Log2CategoryName = GetAppConfigValueFromSiteIfEmpty(Logging.Log2DbCategoryName, "Log2InsuranceCategoryName");

            #endregion

            #region CurrencyToStringFormat
            /// <summary>
            /// Para değerlerinin gösterim formatı
            /// </summary>
            public static readonly string CurrencyToStringFormat = GetAppConfigValueFromSiteIfEmpty(Web.SiteCurrencyToStringFormat, "FlightCurrencyToStringFormat");

            #endregion

            #region Is Enable
            ///<summary>
            /// Loglamanın yapılıp yapılamayacağını belirtir
            ///</summary>
            public static bool EnableLog
            {
                get
                {
                    var rootEnable = Logging.EnableLog;

                    var isEnable = GetBooleanValue("EnableInsuranceLog");

                    return isEnable && rootEnable;
                }
            }

            ///<summary>
            /// Validasyonunun yapılıp yapılamayacağını belirtir
            ///</summary>
            public static bool EnableValidation
            {
                get
                {
                    var rootEnable = Validations.EnableValidations;

                    var isEnable = GetBooleanValue("EnableInsuranceValidations");

                    return isEnable && rootEnable;
                }
            }
            #endregion

            /// <summary>
            /// ModuleName
            /// </summary>
            public static readonly string ModuleName = "Insurance";//TODO:Db deki module tablosundan almalı

            /// <summary>
            /// Log2MailFrom
            /// </summary>
            public static readonly string Log2MailFrom = ConfigurationManager.AppSettings[ModuleName + "Log2MailFrom"];

            #region Log2MailToList
            /// <summary>
            /// Log2MailToList
            /// </summary>
            public static readonly string Log2MailToList = ConfigurationManager.AppSettings[ModuleName + "Log2MailToList"];

            #endregion

            ///<summary>
            /// Servis modu (Test/Prod)
            ///</summary>
            public static bool ServiceModeInTest
            {
                get
                {
                    var rootEnable = Web.ServiceModeInTest;

                    var serviceMode = GetBooleanValue(ModuleName + "ServiceMode");

                    return serviceMode && rootEnable;
                }
            }

            ///<summary>
            /// GenelYasam
            ///</summary>
            public class GenelYasam
            {
                ///<summary>
                /// Logo
                ///</summary>
                public static readonly string LogoGenelYasam = ConfigurationManager.AppSettings["LogoGenelYasam"];

                ///<summary>
                /// DI içerisine register edildiği isim
                ///</summary>
                public static readonly string ServiceRegisterName = "GenelYasam";

                ///<summary>
                /// UserName
                ///</summary>
                public static readonly string UserName = ConfigurationManager.AppSettings["GenelYasamUserName"];

                ///<summary>
                /// Password
                ///</summary>
                public static readonly string Password = ConfigurationManager.AppSettings["GenelYasamPassword"];

                ///<summary>
                /// Url
                ///</summary>
                public static readonly string Url = ConfigurationManager.AppSettings["GenelYasamUrl"];

                ///<summary>
                /// THYCurrencyConverter
                ///</summary>
                public static readonly string CurrencyConverter = ConfigurationManager.AppSettings["GenelYasamCurrencyConverter"];

                ///<summary>
                /// DefaultCurrencySymbol
                ///</summary>
                public static readonly string DefaultCurrencySymbol = GetAppConfigValueFromSiteIfEmpty(Web.SiteDefaultCurrencySymbol, "GenelYasamDefaultCurrencySymbol");

                ///<summary>
                /// DefaultSiteCulture
                ///</summary>
                public static readonly string DefaultSiteCulture = GetAppConfigValueFromSiteIfEmpty(Web.SiteCulture, "GenelYasamDefaultSiteCulture");

                ///<summary>
                /// Loglamanın nereye yapılacağını belirtir
                ///</summary>
                public static readonly string LoggerName = GetAppConfigValueFromSiteIfEmpty(FlightProviders.Log2CategoryName, "GenelYasamLogger");

                ///<summary>
                /// Loglamanın yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableLog
                {
                    get
                    {
                        var rootEnable = InsuranceProviders.EnableLog;

                        var isEnable = GetBooleanValue("GenelYasamEnableLog");

                        return isEnable && rootEnable;
                    }
                }

                ///<summary>
                /// Validasyonunun yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableValidation
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableValidation;

                        var isEnable = GetBooleanValue("GenelYasamEnableValidations");

                        return isEnable && rootEnable;
                    }
                }

                #region Log2MailToList
                /// <summary>
                /// Log2MailToList
                /// </summary>
                public static readonly string Log2MailToList = GetLog2MailToListByProvider(InsuranceProviders.Log2MailToList, ServiceRegisterName);

                #endregion

                ///<summary>
                /// Servis modu (Test/Prod)
                ///</summary>
                public static bool ServiceModeInTest
                {
                    get
                    {
                        var rootEnable = Web.ServiceModeInTest;

                        var serviceMode = GetBooleanValue(ServiceRegisterName + "ServiceMode");

                        return serviceMode && rootEnable;
                    }
                }

                #region CurrencyToStringFormat
                /// <summary>
                /// Para değerlerinin gösterim formatı
                /// </summary>
                public static readonly string CurrencyToStringFormat = GetAppConfigValueFromSiteIfEmpty(InsuranceProviders.CurrencyToStringFormat, "GenelYasamCurrencyToStringFormat");

                #endregion
            }

            ///<summary>
            /// Erv Sigorta
            ///</summary>
            public class Erv
            {
                ///<summary>
                /// Logo
                ///</summary>
                public static readonly string LogoErv = ConfigurationManager.AppSettings["LogoErv"];

                ///<summary>
                /// AgencyNr
                ///</summary>
                public static readonly string AgencyNr = ConfigurationManager.AppSettings["AgencyNr"];

                ///<summary>
                /// ProviderCountry
                ///</summary>
                public static readonly string ProviderCountry = ConfigurationManager.AppSettings["ProviderCountry"];

                ///<summary>
                /// Lang
                ///</summary>
                public static readonly string Lang = ConfigurationManager.AppSettings["Lang"];

                ///<summary>
                /// DI içerisine register edildiği isim
                ///</summary>
                public static readonly string ServiceRegisterName = "Erv";

                ///<summary>
                /// UserName
                ///</summary>
                public static readonly string UserName = ConfigurationManager.AppSettings["ErvUserName"];

                ///<summary>
                /// Password
                ///</summary>
                public static readonly string Password = ConfigurationManager.AppSettings["ErvPassword"];

                ///<summary>
                /// Url
                ///</summary>
                public static readonly string Url = ConfigurationManager.AppSettings["ErvUrl"];

                ///<summary>
                /// THYCurrencyConverter
                ///</summary>
                public static readonly string CurrencyConverter = ConfigurationManager.AppSettings["ErvCurrencyConverter"];

                ///<summary>
                /// DefaultCurrencySymbol
                ///</summary>
                public static readonly string DefaultCurrencySymbol = GetAppConfigValueFromSiteIfEmpty(Web.SiteDefaultCurrencySymbol, "ErvDefaultCurrencySymbol");

                ///<summary>
                /// DefaultSiteCulture
                ///</summary>
                public static readonly string DefaultSiteCulture = GetAppConfigValueFromSiteIfEmpty(Web.SiteCulture, "ErvDefaultSiteCulture");

                ///<summary>
                /// Loglamanın nereye yapılacağını belirtir
                ///</summary>
                public static readonly string LoggerName = GetAppConfigValueFromSiteIfEmpty(FlightProviders.Log2CategoryName, "ErvLogger");

                ///<summary>
                /// Loglamanın yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableLog
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableLog;

                        var isEnable = GetBooleanValue("ErvEnableLog");

                        return isEnable && rootEnable;
                    }
                }

                ///<summary>
                /// Validasyonunun yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableValidation
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableValidation;

                        var isEnable = GetBooleanValue("ErvEnableValidations");

                        return isEnable && rootEnable;
                    }
                }

                #region Log2MailToList
                /// <summary>
                /// Log2MailToList
                /// </summary>
                public static readonly string Log2MailToList = GetLog2MailToListByProvider(InsuranceProviders.Log2MailToList, ServiceRegisterName);

                #endregion

                ///<summary>
                /// Servis modu (Test/Prod)
                ///</summary>
                public static bool ServiceModeInTest
                {
                    get
                    {
                        var rootEnable = Web.ServiceModeInTest;

                        var serviceMode = GetBooleanValue(ServiceRegisterName + "ServiceMode");

                        return serviceMode && rootEnable;
                    }
                }

                #region CurrencyToStringFormat
                /// <summary>
                /// Para değerlerinin gösterim formatı
                /// </summary>
                public static readonly string CurrencyToStringFormat = GetAppConfigValueFromSiteIfEmpty(InsuranceProviders.CurrencyToStringFormat, "ErvCurrencyToStringFormat");

                #endregion
            }

            ///<summary>
            /// AmadeusInsurance Sigorta
            ///</summary>
            public class AmadeusInsurance
            {
                ///<summary>
                /// DI içerisine register edildiği isim
                ///</summary>
                public static readonly string ServiceRegisterName = "AmadeusInsurance";

                ///<summary>
                /// Logo
                ///</summary>
                public static readonly string Logo = ConfigurationManager.AppSettings["LogoAmadeusInsurance"];

                ///<summary>
                /// UserName
                ///</summary>
                public static readonly string UserName = ConfigurationManager.AppSettings["AmadeusInsuranceUserName"];

                ///<summary>
                /// Password
                ///</summary>
                public static readonly string Password = ConfigurationManager.AppSettings["AmadeusInsurancePassword"];

                ///<summary>
                /// Url
                ///</summary>
                public static readonly string Url = ConfigurationManager.AppSettings["AmadeusInsuranceUrl"];

                ///<summary>
                /// AmadeusCurrencyConverter
                ///</summary>
                public static readonly string CurrencyConverter = ConfigurationManager.AppSettings["AmadeusInsuranceCurrencyConverter"];

                ///<summary>
                /// DefaultCurrencySymbol
                ///</summary>
                public static readonly string DefaultCurrencySymbol = GetAppConfigValueFromSiteIfEmpty(Web.SiteDefaultCurrencySymbol, "AmadeusInsuranceDefaultCurrencySymbol");

                ///<summary>
                /// DefaultSiteCulture
                ///</summary>
                public static readonly string DefaultSiteCulture = GetAppConfigValueFromSiteIfEmpty(Web.SiteCulture, "AmadeusInsuranceDefaultSiteCulture");

                ///<summary>
                /// Yüzde komisyon
                ///</summary>
                public static readonly string CommissionPercentage = ConfigurationManager.AppSettings["AmadeusInsuranceCommissionPercentage"];

                ///<summary>
                /// Amadeus Session Alive Time Limit
                ///</summary>
                public static readonly string SessionTimeOutLimitAsSecond = ConfigurationManager.AppSettings["AmadeusInsuranceSessionTimeOutLimitAsSecond"];

                ///<summary>
                /// Loglamanın nereye yapılacağını belirtir
                ///</summary>
                public static readonly string LoggerName = GetAppConfigValueFromSiteIfEmpty(FlightProviders.Log2CategoryName, "AmadeusInsuranceLogger");

                ///<summary>
                /// Loglamanın yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableLog
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableLog;

                        var isEnable = GetBooleanValue("AmadeusInsuranceEnableLog");

                        return isEnable && rootEnable;
                    }
                }

                ///<summary>
                /// Validasyonunun yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableValidation
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableValidation;

                        var isEnable = GetBooleanValue("AmadeusInsuranceEnableValidations");

                        return isEnable && rootEnable;
                    }
                }

                ///<summary>
                /// Servis modu (Test/Prod)
                ///</summary>
                public static bool ServiceModeInTest
                {
                    get
                    {
                        var rootEnable = FlightProviders.ServiceModeInTest;

                        var serviceMode = ConfigurationManager.AppSettings["AmadeusInsuranceFlightServiceMode"];

                        return rootEnable && serviceMode == "Test";
                    }
                }

                #region Log2MailToList

                /// <summary>
                /// Log2MailToList
                /// </summary>
                public static readonly string Log2MailToList = GetLog2MailToListByProvider(InsuranceProviders.Log2MailToList, ServiceRegisterName);

                #endregion

                #region CurrencyToStringFormat
                /// <summary>
                /// Para değerlerinin gösterim formatı
                /// </summary>
                public static readonly string CurrencyToStringFormat = GetAppConfigValueFromSiteIfEmpty(FlightProviders.CurrencyToStringFormat, "AmadeusInsuranceCurrencyToStringFormat");

                #endregion
            }

            /// <summary>
            /// ChartisInsurance Sigorta
            /// </summary>
            public class ChartisInsurance
            {
                ///<summary>
                /// DI içerisine register edildiği isim
                ///</summary>
                public static readonly string ServiceRegisterName = "ChartisInsurance";

                ///<summary>
                /// Logo
                ///</summary>
                public static readonly string Logo = ConfigurationManager.AppSettings["LogoChartisInsurance"];

                ///<summary>
                /// UserName
                ///</summary>
                public static readonly string UserName = ConfigurationManager.AppSettings["ChartisInsuranceUserName"];

                ///<summary>
                /// Password
                ///</summary>
                public static readonly string Password = ConfigurationManager.AppSettings["ChartisInsurancePassword"];

                ///<summary>
                /// Url
                ///</summary>
                public static readonly string Url = ConfigurationManager.AppSettings["ChartisInsuranceUrl"];

                ///<summary>
                /// AmadeusCurrencyConverter
                ///</summary>
                public static readonly string CurrencyConverter = ConfigurationManager.AppSettings["ChartisInsuranceCurrencyConverter"];

                ///<summary>
                /// DefaultCurrencySymbol
                ///</summary>
                public static readonly string DefaultCurrencySymbol = GetAppConfigValueFromSiteIfEmpty(Web.SiteDefaultCurrencySymbol, "ChartisInsuranceDefaultCurrencySymbol");

                ///<summary>
                /// DefaultSiteCulture
                ///</summary>
                public static readonly string DefaultSiteCulture = GetAppConfigValueFromSiteIfEmpty(Web.SiteCulture, "ChartisInsuranceDefaultSiteCulture");

                ///<summary>
                /// Yüzde komisyon
                ///</summary>
                public static readonly string CommissionPercentage = ConfigurationManager.AppSettings["ChartisInsuranceCommissionPercentage"];

                ///<summary>
                /// Amadeus Session Alive Time Limit
                ///</summary>
                public static readonly string SessionTimeOutLimitAsSecond = ConfigurationManager.AppSettings["ChartisInsuranceSessionTimeOutLimitAsSecond"];

                ///<summary>
                /// Loglamanın nereye yapılacağını belirtir
                ///</summary>
                public static readonly string LoggerName = GetAppConfigValueFromSiteIfEmpty(FlightProviders.Log2CategoryName, "ChartisInsuranceLogger");

                /// <summary>
                /// Chartise özel GDS kodu için
                /// </summary>
                public static readonly string ChartisGdsCode = ConfigurationManager.AppSettings["ChartisInsuranceGdsCode"];

                ///<summary>
                /// Loglamanın yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableLog
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableLog;

                        var isEnable = GetBooleanValue("ChartisInsuranceEnableLog");

                        return isEnable && rootEnable;
                    }
                }

                ///<summary>
                /// Validasyonunun yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableValidation
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableValidation;

                        var isEnable = GetBooleanValue("ChartisInsuranceEnableValidations");

                        return isEnable && rootEnable;
                    }
                }

                ///<summary>
                /// Servis modu (Test/Prod)
                ///</summary>
                public static bool ServiceModeInTest
                {
                    get
                    {
                        var rootEnable = FlightProviders.ServiceModeInTest;

                        var serviceMode = ConfigurationManager.AppSettings["ChartisInsuranceFlightServiceMode"];

                        return rootEnable && serviceMode == "Test";
                    }
                }

                #region Log2MailToList

                /// <summary>
                /// Log2MailToList
                /// </summary>
                public static readonly string Log2MailToList = GetLog2MailToListByProvider(InsuranceProviders.Log2MailToList, ServiceRegisterName);

                #endregion

                #region CurrencyToStringFormat
                /// <summary>
                /// Para değerlerinin gösterim formatı
                /// </summary>
                public static readonly string CurrencyToStringFormat = GetAppConfigValueFromSiteIfEmpty(FlightProviders.CurrencyToStringFormat, "ChartisInsuranceCurrencyToStringFormat");

                #endregion
            }

        }

        ///<summary>
        ///</summary>
        public sealed class TourProviders
        {
            #region Log2
            ///<summary>
            /// Log2CategoryName
            ///</summary>
            public static readonly string Log2CategoryName = GetAppConfigValueFromSiteIfEmpty(Logging.Log2DbCategoryName, "Log2TourCategoryName");

            #endregion

            #region Is Enable
            ///<summary>
            /// Loglamanın yapılıp yapılamayacağını belirtir
            ///</summary>
            public static bool EnableLog
            {
                get
                {
                    var rootEnable = Logging.EnableLog;

                    var isEnable = GetBooleanValue("EnableTourLog");

                    return isEnable && rootEnable;
                }
            }

            ///<summary>
            /// Validasyonunun yapılıp yapılamayacağını belirtir
            ///</summary>
            public static bool EnableValidation
            {
                get
                {
                    var rootEnable = Validations.EnableValidations;

                    var isEnable = GetBooleanValue("EnableTourValidations");

                    return isEnable && rootEnable;
                }
            }
            #endregion

            #region CurrencyToStringFormat
            /// <summary>
            /// Para değerlerinin gösterim formatı
            /// </summary>
            public static readonly string CurrencyToStringFormat = GetAppConfigValueFromSiteIfEmpty(Web.SiteCurrencyToStringFormat, "TourCurrencyToStringFormat");

            #endregion

            /// <summary>
            /// ModuleName
            /// </summary>
            public static readonly string ModuleName = "Tour";//TODO:Db deki module tablosundan almalı

            #region Log2MailToList
            /// <summary>
            /// Log2MailToList
            /// </summary>
            public static readonly string Log2MailToList = ConfigurationManager.AppSettings[ModuleName + "Log2MailToList"];

            /// <summary>
            /// Log2MailFrom
            /// </summary>
            public static readonly string Log2MailFrom = ConfigurationManager.AppSettings[ModuleName + "Log2MailFrom"];

            /// <summary>
            /// MailSubjectKey
            /// </summary>
            public static readonly string MailSubjectKey = ConfigurationManager.AppSettings[ModuleName + "MailSubjectKey"];

            #endregion

            ///<summary>
            /// Servis modu (Test/Prod)
            ///</summary>
            public static bool ServiceModeInTest
            {
                get
                {
                    var rootEnable = Web.ServiceModeInTest;

                    var serviceMode = GetBooleanValue(ModuleName + "ServiceMode");

                    return serviceMode && rootEnable;
                }
            }

            /// <summary>
            /// Tur seviyesinde FareBasis alınıp alınmayacağı
            /// </summary>
            public static bool EnableFareBasis
            {
                get
                {
                    var siteEnable = Web.EnableFareBasis;
                    var isEnable = GetBooleanValue(ModuleName + "EnableFareBasis", false);

                    return isEnable && siteEnable;
                }
            }

            ///<summary>
            /// Pronto
            ///</summary>
            public class Pronto
            {
                ///<summary>
                /// Logo
                ///</summary>
                public static readonly string AgencyCode = ConfigurationManager.AppSettings["ProntoAgencyCode"];

                ///<summary>
                /// Logo
                ///</summary>
                public static readonly string Language = ConfigurationManager.AppSettings["ProntoLanguage"];

                ///<summary>
                /// Logo
                ///</summary>
                public static readonly string LogoGenelYasam = ConfigurationManager.AppSettings["LogoPronto"];

                ///<summary>
                /// DI içerisine register edildiği isim
                ///</summary>
                public static readonly string ServiceRegisterName = "Pronto";

                ///<summary>
                /// UserName
                ///</summary>
                public static readonly string UserName = ConfigurationManager.AppSettings["ProntoUserName"];

                ///<summary>
                /// Password
                ///</summary>
                public static readonly string Password = ConfigurationManager.AppSettings["ProntoPassword"];

                ///<summary>
                /// Url
                ///</summary>
                public static readonly string Url = ConfigurationManager.AppSettings["ProntoUrl"];

                ///<summary>
                /// THYCurrencyConverter
                ///</summary>
                public static readonly string CurrencyConverter = ConfigurationManager.AppSettings["ProntoCurrencyConverter"];

                ///<summary>
                /// DefaultCurrencySymbol
                ///</summary>
                public static readonly string DefaultCurrencySymbol = GetAppConfigValueFromSiteIfEmpty(Web.SiteDefaultCurrencySymbol, "ProntoDefaultCurrencySymbol");

                ///<summary>
                /// DefaultSiteCulture
                ///</summary>
                public static readonly string DefaultSiteCulture = GetAppConfigValueFromSiteIfEmpty(Web.SiteCulture, "ProntoDefaultSiteCulture");

                ///<summary>
                /// Loglamanın nereye yapılacağını belirtir
                ///</summary>
                public static readonly string LoggerName = GetAppConfigValueFromSiteIfEmpty(FlightProviders.Log2CategoryName, "ProntoLogger");

                ///<summary>
                /// City tablosu için mapping değerleri
                ///</summary>
                public static readonly string MappingCities = ConfigurationManager.AppSettings["ProntoTourMappingCities"];

                ///<summary>
                /// Country tablosu için mapping değerleri
                ///</summary>
                public static readonly string MappingCountries = ConfigurationManager.AppSettings["ProntoTourMappingCountries"];

                ///<summary>
                /// Loglamanın yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableLog
                {
                    get
                    {
                        var rootEnable = InsuranceProviders.EnableLog;

                        var isEnable = GetBooleanValue("ProntoEnableLog");

                        return isEnable && rootEnable;
                    }
                }

                ///<summary>
                /// Validasyonunun yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableValidation
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableValidation;

                        var isEnable = GetBooleanValue("ProntoEnableValidations");

                        return isEnable && rootEnable;
                    }
                }

                #region Log2MailToList
                /// <summary>
                /// Log2MailToList
                /// </summary>
                public static readonly string Log2MailToList = GetLog2MailToListByProvider(TourProviders.Log2MailToList, ServiceRegisterName);

                #endregion

                ///<summary>
                /// Servis modu (Test/Prod)
                ///</summary>
                public static bool ServiceModeInTest
                {
                    get
                    {
                        var rootEnable = Web.ServiceModeInTest;

                        var serviceMode = GetBooleanValue(ServiceRegisterName + "ServiceMode");

                        return serviceMode && rootEnable;
                    }
                }

                /// <summary>
                /// Tur Pronto seviyesinde FareBasis alınıp alınmayacağı
                /// </summary>
                public static bool EnableFareBasis
                {
                    get
                    {
                        var siteEnable = TourProviders.EnableFareBasis;
                        var isEnable = GetBooleanValue(ServiceRegisterName + "EnableFareBasis", false);

                        return isEnable && siteEnable;
                    }
                }

                #region CurrencyToStringFormat
                /// <summary>
                /// Para değerlerinin gösterim formatı
                /// </summary>
                public static readonly string CurrencyToStringFormat = GetAppConfigValueFromSiteIfEmpty(TourProviders.CurrencyToStringFormat, "ProntoCurrencyToStringFormat");

                #endregion
            }

        }

        ///<summary>
        /// Otel servisi işlemlerinin static property değerlerine erişimi sağlayan sınıf
        ///</summary>
        public sealed class HotelProviders
        {
            #region Log2
            ///<summary>
            /// Log2CategoryName
            ///</summary>
            public static readonly string Log2CategoryName = GetAppConfigValueFromSiteIfEmpty(Logging.Log2DbCategoryName, "Log2HotelCategoryName");

            #endregion

            #region Is Enable
            ///<summary>
            /// Loglamanın yapılıp yapılamayacağını belirtir
            ///</summary>
            public static bool EnableLog
            {
                get
                {
                    var rootEnable = Logging.EnableLog;

                    var isEnable = GetBooleanValue("EnableHotelLog");

                    return isEnable && rootEnable;
                }
            }

            ///<summary>
            /// Validasyonunun yapılıp yapılamayacağını belirtir
            ///</summary>
            public static bool EnableValidation
            {
                get
                {
                    var rootEnable = Validations.EnableValidations;

                    var isEnable = GetBooleanValue("EnableHotelValidations");

                    return isEnable && rootEnable;
                }
            }
            #endregion

            #region CurrencyToStringFormat
            /// <summary>
            /// Para değerlerinin gösterim formatı
            /// </summary>
            public static readonly string CurrencyToStringFormat = GetAppConfigValueFromSiteIfEmpty(Web.SiteCurrencyToStringFormat, "HotelCurrencyToStringFormat");

            #endregion

            /// <summary>
            /// ModuleName
            /// </summary>
            public static readonly string ModuleName = "Hotel";//TODO:Db deki module tablosundan almalı

            #region Log2MailToList
            /// <summary>
            /// Log2MailToList
            /// </summary>
            public static readonly string Log2MailToList = ConfigurationManager.AppSettings[ModuleName + "Log2MailToList"];

            /// <summary>
            /// Log2MailFrom
            /// </summary>
            public static readonly string Log2MailFrom = ConfigurationManager.AppSettings[ModuleName + "Log2MailFrom"];

            /// <summary>
            /// MailSubjectKey
            /// </summary>
            public static readonly string MailSubjectKey = ConfigurationManager.AppSettings[ModuleName + "MailSubjectKey"];
            #endregion

            ///<summary>
            /// Servis modu (Test/Prod)
            ///</summary>
            public static bool ServiceModeInTest
            {
                get
                {
                    var rootEnable = Web.ServiceModeInTest;

                    var serviceMode = GetBooleanValue(ModuleName + "ServiceMode");

                    return serviceMode && rootEnable;
                }
            }

            /// <summary>
            /// Sigorta alınıp alınmadığına dair parametreyi taşıyacak name
            /// </summary>
            public static string InsuranceParameter
            {
                get
                {
                    string InsuranceName = "HotelInsurance";
                    return InsuranceName;
                }

            }

            ///<summary>
            /// GTA otel servisi için ayarlar
            ///</summary>
            public class GTA
            {
                ///<summary>
                /// DI içerisine register edildiği isim
                ///</summary>
                public static readonly string ServiceRegisterName = "GTAHotel";

                ///<summary>
                /// CountryCode
                ///</summary>
                public static readonly string CountryCode = ConfigurationManager.AppSettings["GTAHotelCountryCode"];

                ///<summary>
                /// Language
                ///</summary>
                public static readonly string Language = ConfigurationManager.AppSettings["GTAHotelLang"];

                ///<summary>
                /// ClientID
                ///</summary>
                public static readonly string ClientID = ConfigurationManager.AppSettings["GTAHotelClientID"];

                ///<summary>
                /// EmailAddress
                ///</summary>
                public static readonly string EmailAddress = ConfigurationManager.AppSettings["GTAHotelEmailAddress"];

                ///<summary>
                /// Password
                ///</summary>
                public static readonly string Password = ConfigurationManager.AppSettings["GTAHotelPassword"];

                ///<summary>
                /// Url
                ///</summary>
                public static readonly string Url = ConfigurationManager.AppSettings["GTAHotelUrl"];

                ///<summary>
                /// GTANumberOfReturnedItems
                ///</summary>
                public static readonly string NumberOfReturnedItems = ConfigurationManager.AppSettings["GTAHotelNumberOfReturnedItems"];

                ///<summary>
                /// CurrencyConverter
                ///</summary>
                public static readonly string CurrencyConverter = ConfigurationManager.AppSettings["GTAHotelCurrencyConverter"];

                ///<summary>
                /// DefaultCurrencySymbol
                ///</summary>
                public static readonly string DefaultCurrencySymbol = GetAppConfigValueFromSiteIfEmpty(Web.SiteDefaultCurrencySymbol, "GTAHotelDefaultCurrencySymbol");

                ///<summary>
                /// DefaultSiteCulture
                ///</summary>
                public static readonly string DefaultSiteCulture = GetAppConfigValueFromSiteIfEmpty(Web.SiteCulture, "GTAHotelDefaultSiteCulture");

                ///<summary>
                /// Loglamanın nereye yapılacağını belirtir
                ///</summary>
                public static readonly string LoggerName = GetAppConfigValueFromSiteIfEmpty(HotelProviders.Log2CategoryName, "GTAHotelLogger");

                /// <summary>
                /// Ürün üzerine uygulanacak komisyon oranı 
                /// </summary>
                public static readonly string CommissionPercentage = ConfigurationManager.AppSettings["GTAHotelCommissionPercentage"];
                ///<summary>
                /// Loglamanın yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableLog
                {
                    get
                    {
                        var rootEnable = HotelProviders.EnableLog;

                        var isEnable = GetBooleanValue("GTAHotelEnableLog");

                        return isEnable && rootEnable;
                    }
                }

                ///<summary>
                /// Validasyonunun yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableValidation
                {
                    get
                    {
                        var rootEnable = HotelProviders.EnableValidation;

                        var isEnable = GetBooleanValue("GTAHotelEnableValidations");

                        return isEnable && rootEnable;
                    }
                }

                #region Log2MailToList
                /// <summary>
                /// Log2MailToList
                /// </summary>
                public static readonly string Log2MailToList = GetLog2MailToListByProvider(HotelProviders.Log2MailToList, ServiceRegisterName);

                #endregion

                ///<summary>
                /// Otel'in arama yapacağı lokasyon tipini belirtir. YI/YD/All => degerlerini alir (YI:Yurtici-YD:YurtDisi-All:Tumu)
                ///</summary>
                public static string Direction
                {
                    get
                    {
                        var returnVal = ConfigurationManager.AppSettings[ServiceRegisterName + "Direction"];

                        return returnVal;
                    }
                }

                #region CurrencyToStringFormat
                /// <summary>
                /// Para değerlerinin gösterim formatı
                /// </summary>
                public static readonly string CurrencyToStringFormat = GetAppConfigValueFromSiteIfEmpty(HotelProviders.CurrencyToStringFormat, "GTAHotelCurrencyToStringFormat");

                #endregion
            }

            public class Expedia
            {

                /// <summary>
                /// 
                /// </summary>
                public static readonly string ApiKey = ConfigurationManager.AppSettings["ExpediaApiKey"];

                /// <summary>
                /// 
                /// </summary>
                public static readonly long CID = Convert.ToInt64(ConfigurationManager.AppSettings["ExpediaCID"]);

                /// <summary>
                /// Servisten talep edilen datanın para birimi
                /// </summary>
                public static readonly string ServiceRequestCurrencyCode = ConfigurationManager.AppSettings["ExpediaHotelServiceRequestCurrencyCode"];

                ///<summary>
                /// DI içerisine register edildiği isim
                ///</summary>
                public static readonly string ServiceRegisterName = "ExpediaHotel";

                ///<summary>
                /// CountryCode
                ///</summary>
                public static readonly string CountryCode = ConfigurationManager.AppSettings["ExpediaHotelCountryCode"];

                ///<summary>
                /// Language
                ///</summary>
                public static readonly string Language = ConfigurationManager.AppSettings["ExpediaHotelLang"];

                ///<summary>
                /// ClientID
                ///</summary>
                public static readonly string ClientID = ConfigurationManager.AppSettings["ExpediaHotelClientID"];

                ///<summary>
                /// EmailAddress
                ///</summary>
                public static readonly string EmailAddress = ConfigurationManager.AppSettings["ExpediaHotelEmailAddress"];

                ///<summary>
                /// Password
                ///</summary>
                public static readonly string Password = ConfigurationManager.AppSettings["ExpediaHotelPassword"];

                ///<summary>
                /// Url
                ///</summary>
                public static readonly string Url = ConfigurationManager.AppSettings["ExpediaHotelUrl"];

                ///<summary>
                /// GTANumberOfReturnedItems
                ///</summary>
                public static readonly string NumberOfReturnedItems = ConfigurationManager.AppSettings["ExpediaHotelNumberOfReturnedItems"];

                ///<summary>
                /// CurrencyConverter
                ///</summary>
                public static readonly string CurrencyConverter = ConfigurationManager.AppSettings["ExpediaHotelCurrencyConverter"];

                ///<summary>
                /// DefaultCurrencySymbol
                ///</summary>
                public static readonly string DefaultCurrencySymbol = GetAppConfigValueFromSiteIfEmpty(Web.SiteDefaultCurrencySymbol, "ExpediaHotelDefaultCurrencySymbol");

                ///<summary>
                /// DefaultSiteCulture
                ///</summary>
                public static readonly string DefaultSiteCulture = GetAppConfigValueFromSiteIfEmpty(Web.SiteCulture, "ExpediaHotelDefaultSiteCulture");

                ///<summary>
                /// Loglamanın nereye yapılacağını belirtir
                ///</summary>
                public static readonly string LoggerName = GetAppConfigValueFromSiteIfEmpty(HotelProviders.Log2CategoryName, "ExpediaHotelLogger");


                ///<summary>
                /// Uygulamada gerçek kredi kartı bilgileri kullanılıyor mu?
                ///</summary>
                public static bool LiveCreditCard
                {
                    get
                    {
                        var isEnable = GetBooleanValue("ExpediaHotelLiveCreditCard");
                        return isEnable;
                    }
                }

                ///<summary>
                /// Fatura mail adresi
                ///</summary>
                public static readonly string BillingEmail = ConfigurationManager.AppSettings["ExpediaBillingEmail"];

                ///<summary>
                /// Fatura adresi
                ///</summary>
                public static readonly string BillingAddress = ConfigurationManager.AppSettings["ExpediaBillingAddress"];


                ///<summary>
                /// Fatura şehir bilgisi
                ///</summary>
                public static readonly string BillingCity = ConfigurationManager.AppSettings["ExpediaBillingCity"];

                ///<summary>
                /// Fatura posta kodu
                ///</summary>
                public static readonly string BillingPostalCode = ConfigurationManager.AppSettings["ExpediaBillingPostalCode"];

                ///<summary>
                /// Fatura ülke kodu
                ///</summary>
                public static readonly string BillingCountryCode = ConfigurationManager.AppSettings["ExpediaBillingCountryCode"];

                ///<summary>
                /// Fatura isim 
                ///</summary>
                public static readonly string BillingFistName = ConfigurationManager.AppSettings["ExpediaBillingFistName"];

                ///<summary>
                /// Fatura soyad 
                ///</summary>
                public static readonly string BillingLastName = ConfigurationManager.AppSettings["ExpediaBillingLastName"];

                ///<summary>
                /// Fatura şirket adı 
                ///</summary>
                public static readonly string BillingCompanyName = ConfigurationManager.AppSettings["ExpediaBillingCompanyName"];

                ///<summary>
                /// Fatura telefon 
                ///</summary>
                public static readonly string BillingWorkPhone = ConfigurationManager.AppSettings["ExpediaBillingWorkPhone"];

                ///<summary>
                /// Fatura telefon 
                ///</summary>
                public static readonly string CancellationDay = ConfigurationManager.AppSettings["ExpediaCancellationDay"];

                /// <summary>
                /// Ürün üzerine uygulanacak komisyon oranı 
                /// </summary>
                public static readonly string CommissionPercentage = ConfigurationManager.AppSettings["ExpediaHotelCommissionPercentage"];

                ///<summary>
                /// Loglamanın yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableLog
                {
                    get
                    {
                        var rootEnable = HotelProviders.EnableLog;

                        var isEnable = GetBooleanValue("ExpediaHotelEnableLog");

                        return isEnable && rootEnable;
                    }
                }

                ///<summary>
                /// Validasyonunun yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableValidation
                {
                    get
                    {
                        var rootEnable = HotelProviders.EnableValidation;

                        var isEnable = GetBooleanValue("ExpediaHotelEnableValidations");

                        return isEnable && rootEnable;
                    }
                }

                ///<summary>
                /// Otel'in arama yapacağı lokasyon tipini belirtir. YI/YD/All => degerlerini alir (YI:Yurtici-YD:YurtDisi-All:Tumu)
                ///</summary>
                public static string Direction
                {
                    get
                    {
                        var returnVal = ConfigurationManager.AppSettings[ServiceRegisterName + "Direction"];

                        return returnVal;
                    }
                }

                #region Log2MailToList
                /// <summary>
                /// Log2MailToList
                /// </summary>
                public static readonly string Log2MailToList = GetLog2MailToListByProvider(HotelProviders.Log2MailToList, ServiceRegisterName);

                #endregion

                ///<summary>
                /// Servis modu (Test/Prod)
                ///</summary>
                public static bool ServiceModeInTest
                {
                    get
                    {
                        var rootEnable = Web.ServiceModeInTest;

                        var serviceMode = GetBooleanValue(ServiceRegisterName + "ServiceMode");

                        return serviceMode && rootEnable;
                    }
                }

                #region CurrencyToStringFormat
                /// <summary>
                /// Para değerlerinin gösterim formatı
                /// </summary>
                public static readonly string CurrencyToStringFormat = GetAppConfigValueFromSiteIfEmpty(HotelProviders.CurrencyToStringFormat, "ExpediaHotelCurrencyToStringFormat");

                #endregion

            }

            public class Jolly
            {

                ///<summary>
                /// DI içerisine register edildiği isim
                ///</summary>
                public static readonly string ServiceRegisterName = "JollyHotel";

                /// <summary>
                /// Servisten talep edilen datanın para birimi
                /// </summary>
                public static readonly string ServiceRequestCurrencyCode = ConfigurationManager.AppSettings[ServiceRegisterName + "ServiceRequestCurrencyCode"];


                ///<summary>
                /// Password
                ///</summary>
                public static readonly string Password = ConfigurationManager.AppSettings[ServiceRegisterName + "Password"];

                ///<summary>
                /// UserName
                ///</summary>
                public static readonly string UserName = ConfigurationManager.AppSettings[ServiceRegisterName + "UserName"];

                ///<summary>
                /// UserName
                ///</summary>
                public static readonly string Url = ConfigurationManager.AppSettings[ServiceRegisterName + "Url"];

                ///<summary>
                /// UserName
                ///</summary>
                public static readonly string BookUrl = ConfigurationManager.AppSettings[ServiceRegisterName + "BookUrl"];


                ///<summary>
                /// jollyNumberOfReturnedItems
                ///</summary>
                public static readonly string NumberOfReturnedItems = ConfigurationManager.AppSettings[ServiceRegisterName + "NumberOfReturnedItems"];

                ///<summary>
                /// CurrencyConverter
                ///</summary>
                public static readonly string CurrencyConverter = ConfigurationManager.AppSettings[ServiceRegisterName + "CurrencyConverter"];

                ///<summary>
                /// DefaultCurrencySymbol
                ///</summary>
                public static readonly string DefaultCurrencySymbol = GetAppConfigValueFromSiteIfEmpty(Web.SiteDefaultCurrencySymbol, ServiceRegisterName + "DefaultCurrencySymbol");

                ///<summary>
                /// DefaultSiteCulture
                ///</summary>
                public static readonly string DefaultSiteCulture = GetAppConfigValueFromSiteIfEmpty(Web.SiteCulture, ServiceRegisterName + "DefaultSiteCulture");

                ///<summary>
                /// Loglamanın nereye yapılacağını belirtir
                ///</summary>
                public static readonly string LoggerName = GetAppConfigValueFromSiteIfEmpty(FlightProviders.Log2CategoryName, ServiceRegisterName + "Logger");

                /// <summary>
                /// Ürün üzerine uygulanacak komisyon oranı 
                /// </summary>
                public static readonly string CommissionPercentage = ConfigurationManager.AppSettings[ServiceRegisterName + "CommissionPercentage"];

                ///<summary>
                /// Loglamanın yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableLog
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableLog;

                        var isEnable = GetBooleanValue(ServiceRegisterName + "EnableLog");

                        return isEnable && rootEnable;
                    }
                }

                ///<summary>
                /// Validasyonunun yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableValidation
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableValidation;

                        var isEnable = GetBooleanValue(ServiceRegisterName + "EnableValidations");

                        return isEnable && rootEnable;
                    }
                }

                #region Log2MailToList
                /// <summary>
                /// Log2MailToList
                /// </summary>
                public static readonly string Log2MailToList = GetLog2MailToListByProvider(HotelProviders.Log2MailToList, ServiceRegisterName);

                #endregion

                ///<summary>
                /// Servis modu (Test/Prod)
                ///</summary>
                public static bool ServiceModeInTest
                {
                    get
                    {
                        var rootEnable = Web.ServiceModeInTest;

                        var serviceMode = GetBooleanValue(ServiceRegisterName + "ServiceMode");

                        return serviceMode && rootEnable;
                    }
                }

                ///<summary>
                /// Otel'in arama yapacağı lokasyon tipini belirtir. YI/YD/All => degerlerini alir (YI:Yurtici-YD:YurtDisi-All:Tumu)
                ///</summary>
                public static string Direction
                {
                    get
                    {
                        var returnVal = ConfigurationManager.AppSettings[ServiceRegisterName + "Direction"];

                        return returnVal;
                    }
                }

                #region CurrencyToStringFormat
                /// <summary>
                /// Para değerlerinin gösterim formatı
                /// </summary>
                public static readonly string CurrencyToStringFormat = GetAppConfigValueFromSiteIfEmpty(HotelProviders.CurrencyToStringFormat, "JollyHotelCurrencyToStringFormat");

                #endregion

            }

            #region TripAdvisor
            ///<summary>
            /// TripAdvisor'un bize ilettiği zip dosyasının URL'si
            ///</summary>
            public static readonly string TripAdvisorPlaceFileURL = ConfigurationManager.AppSettings[ModuleName + "TripAdvisorPlaceFileURL"];

            ///<summary>
            /// Zipli dosyanın server üzerinde downlad edileceği path
            ///</summary>
            public static readonly string TripAdvisorZippedFolderPath = ConfigurationManager.AppSettings[ModuleName + "TripAdvisorZippedFolderPath"];

            ///<summary>
            /// Zipli dosyanın server üzerinde zipten çıkarılacağı path
            ///</summary>
            public static readonly string TripAdvisorUnZippedFolderPath = ConfigurationManager.AppSettings[ModuleName + "TripAdvisorUnZippedFolderPath"];
            #endregion
        }

        ///<summary>
        /// Sehir Turları
        ///</summary>
        public sealed class SightSeeingProviders
        {
            #region Log2
            ///<summary>
            /// Log2CategoryName
            ///</summary>
            public static readonly string Log2CategoryName = GetAppConfigValueFromSiteIfEmpty(Logging.Log2DbCategoryName, "Log2SightSeeingCategoryName");

            #endregion

            #region Is Enable
            ///<summary>
            /// Loglamanın yapılıp yapılamayacağını belirtir
            ///</summary>
            public static bool EnableLog
            {
                get
                {
                    var rootEnable = Logging.EnableLog;

                    var isEnable = GetBooleanValue("EnableSightSeeingLog");

                    return isEnable && rootEnable;
                }
            }

            ///<summary>
            /// Validasyonunun yapılıp yapılamayacağını belirtir
            ///</summary>
            public static bool EnableValidation
            {
                get
                {
                    var rootEnable = Validations.EnableValidations;

                    var isEnable = GetBooleanValue("EnableSightSeeingValidations");

                    return isEnable && rootEnable;
                }
            }
            #endregion

            #region CurrencyToStringFormat
            /// <summary>
            /// Para değerlerinin gösterim formatı
            /// </summary>
            public static readonly string CurrencyToStringFormat = GetAppConfigValueFromSiteIfEmpty(Web.SiteCurrencyToStringFormat, "SightSeeingCurrencyToStringFormat");

            #endregion

            /// <summary>
            /// ModuleName
            /// </summary>
            public static readonly string ModuleName = "SightSeeing";//TODO:Db deki module tablosundan almalı

            #region Log2MailToList
            /// <summary>
            /// Log2MailToList
            /// </summary>
            public static readonly string Log2MailToList = ConfigurationManager.AppSettings[ModuleName + "Log2MailToList"];

            /// <summary>
            /// Log2MailFrom
            /// </summary>
            public static readonly string Log2MailFrom = ConfigurationManager.AppSettings[ModuleName + "Log2MailFrom"];

            /// <summary>
            /// MailSubjectKey
            /// </summary>
            public static readonly string MailSubjectKey = ConfigurationManager.AppSettings[ModuleName + "MailSubjectKey"];
            #endregion

            ///<summary>
            /// Servis modu (Test/Prod)
            ///</summary>
            public static bool ServiceModeInTest
            {
                get
                {
                    var rootEnable = Web.ServiceModeInTest;

                    var serviceMode = GetBooleanValue(ModuleName + "ServiceMode");

                    return serviceMode && rootEnable;
                }
            }

            ///<summary>
            /// GTA SightSeeing servisi için ayarlar
            ///</summary>
            public sealed class GTASightSeeing
            {
                ///<summary>
                /// DI içerisine register edildiği isim
                ///</summary>
                public static readonly string ServiceRegisterName = "GTASightSeeing";

                ///<summary>
                /// CountryCode
                ///</summary>
                public static readonly string CountryCode = ConfigurationManager.AppSettings["GTASightSeeingCountryCode"];

                ///<summary>
                /// Language
                ///</summary>
                public static readonly string Language = ConfigurationManager.AppSettings["GTASightSeeingLang"];

                ///<summary>
                /// ClientID
                ///</summary>
                public static readonly string ClientID = ConfigurationManager.AppSettings["GTASightSeeingClientID"];

                ///<summary>
                /// EmailAddress
                ///</summary>
                public static readonly string EmailAddress = ConfigurationManager.AppSettings["GTASightSeeingEmailAddress"];

                ///<summary>
                /// Password
                ///</summary>
                public static readonly string Password = ConfigurationManager.AppSettings["GTASightSeeingPassword"];

                ///<summary>
                /// Url
                ///</summary>
                public static readonly string Url = ConfigurationManager.AppSettings["GTASightSeeingUrl"];

                ///<summary>
                /// CurrencyConverter
                ///</summary>
                public static readonly string CurrencyConverter = ConfigurationManager.AppSettings["GTASightSeeingCurrencyConverter"];

                ///<summary>
                /// DefaultCurrencySymbol
                ///</summary>
                public static readonly string DefaultCurrencySymbol = GetAppConfigValueFromSiteIfEmpty(Web.SiteDefaultCurrencySymbol, "GTASightSeeingDefaultCurrencySymbol");

                ///<summary>
                /// DefaultSiteCulture
                ///</summary>
                public static readonly string DefaultSiteCulture = GetAppConfigValueFromSiteIfEmpty(Web.SiteCulture, "GTASightSeeingDefaultSiteCulture");

                ///<summary>
                /// Loglamanın nereye yapılacağını belirtir
                ///</summary>
                public static readonly string LoggerName = GetAppConfigValueFromSiteIfEmpty(FlightProviders.Log2CategoryName, "GTASightSeeingLogger");

                /// <summary>
                /// Ürün üzerine uygulanacak komisyon oranı 
                /// </summary>
                public static readonly string CommissionPercentage = ConfigurationManager.AppSettings["GTASightSeeingCommissionPercentage"];

                ///<summary>
                /// Loglamanın yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableLog
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableLog;

                        var isEnable = GetBooleanValue("GTASightSeeingEnableLog");

                        return isEnable && rootEnable;
                    }
                }

                ///<summary>
                /// Validasyonunun yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableValidation
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableValidation;

                        var isEnable = GetBooleanValue("GTASightSeeingEnableValidations");

                        return isEnable && rootEnable;
                    }
                }

                #region Log2MailToList
                /// <summary>
                /// Log2MailToList
                /// </summary>
                public static readonly string Log2MailToList = GetLog2MailToListByProvider(SightSeeingProviders.Log2MailToList, ServiceRegisterName);

                #endregion

                ///<summary>
                /// Servis modu (Test/Prod)
                ///</summary>
                public static bool ServiceModeInTest
                {
                    get
                    {
                        var rootEnable = Web.ServiceModeInTest;

                        var serviceMode = GetBooleanValue(ServiceRegisterName + "ServiceMode");

                        return serviceMode && rootEnable;
                    }
                }

                #region CurrencyToStringFormat
                /// <summary>
                /// Para değerlerinin gösterim formatı
                /// </summary>
                public static readonly string CurrencyToStringFormat = GetAppConfigValueFromSiteIfEmpty(FlightProviders.CurrencyToStringFormat, "GTASightSeeingCurrencyToStringFormat");

                /// <summary>
                /// SightSeeingSearchTourLanguage -> None olarak gönderiliyor
                /// </summary>
                public static readonly string SightSeeingSearchTourLanguage = "None";
                #endregion
            }

        }

        ///<summary>
        /// Transfer
        ///</summary>
        public sealed class TransferProviders
        {
            #region Log2
            ///<summary>
            /// Log2CategoryName
            ///</summary>
            public static readonly string Log2CategoryName = GetAppConfigValueFromSiteIfEmpty(Logging.Log2DbCategoryName, "Log2TransferCategoryName");

            #endregion

            #region Is Enable
            ///<summary>
            /// Loglamanın yapılıp yapılamayacağını belirtir
            ///</summary>
            public static bool EnableLog
            {
                get
                {
                    var rootEnable = Logging.EnableLog;

                    var isEnable = GetBooleanValue("EnableTransferLog");

                    return isEnable && rootEnable;
                }
            }

            ///<summary>
            /// Validasyonunun yapılıp yapılamayacağını belirtir
            ///</summary>
            public static bool EnableValidation
            {
                get
                {
                    var rootEnable = Validations.EnableValidations;

                    var isEnable = GetBooleanValue("EnableTransferValidations");

                    return isEnable && rootEnable;
                }
            }
            #endregion

            #region CurrencyToStringFormat
            /// <summary>
            /// Para değerlerinin gösterim formatı
            /// </summary>
            public static readonly string CurrencyToStringFormat = GetAppConfigValueFromSiteIfEmpty(Web.SiteCurrencyToStringFormat, "TransferCurrencyToStringFormat");

            #endregion

            /// <summary>
            /// ModuleName
            /// </summary>
            public static readonly string ModuleName = "Transfer";//TODO:Db deki module tablosundan almalı

            #region Log2MailToList
            /// <summary>
            /// Log2MailToList
            /// </summary>
            public static readonly string Log2MailToList = ConfigurationManager.AppSettings[ModuleName + "Log2MailToList"];

            /// <summary>
            /// Log2MailFrom
            /// </summary>
            public static readonly string Log2MailFrom = ConfigurationManager.AppSettings[ModuleName + "Log2MailFrom"];

            /// <summary>
            /// MailSubjectKey
            /// </summary>
            public static readonly string MailSubjectKey = ConfigurationManager.AppSettings[ModuleName + "MailSubjectKey"];
            #endregion

            ///<summary>
            /// Servis modu (Test/Prod)
            ///</summary>
            public static bool ServiceModeInTest
            {
                get
                {
                    var rootEnable = Web.ServiceModeInTest;

                    var serviceMode = GetBooleanValue(ModuleName + "ServiceMode");

                    return serviceMode && rootEnable;
                }
            }

            ///<summary>
            /// GTA Transfer servisi için ayarlar
            ///</summary>
            public sealed class GTATransfer
            {
                ///<summary>
                /// DI içerisine register edildiği isim
                ///</summary>
                public static readonly string ServiceRegisterName = "GTATransfer";

                ///<summary>
                /// CountryCode
                ///</summary>
                public static readonly string CountryCode = ConfigurationManager.AppSettings["GTATransferCountryCode"];

                ///<summary>
                /// Language
                ///</summary>
                public static readonly string Language = ConfigurationManager.AppSettings["GTATransferLang"];

                ///<summary>
                /// ClientID
                ///</summary>
                public static readonly string ClientID = ConfigurationManager.AppSettings["GTATransferClientID"];

                ///<summary>
                /// EmailAddress
                ///</summary>
                public static readonly string EmailAddress = ConfigurationManager.AppSettings["GTATransferEmailAddress"];

                ///<summary>
                /// Password
                ///</summary>
                public static readonly string Password = ConfigurationManager.AppSettings["GTATransferPassword"];

                ///<summary>
                /// Url
                ///</summary>
                public static readonly string Url = ConfigurationManager.AppSettings["GTATransferUrl"];

                ///<summary>
                /// CurrencyConverter
                ///</summary>
                public static readonly string CurrencyConverter = ConfigurationManager.AppSettings["GTATransferCurrencyConverter"];

                ///<summary>
                /// DefaultCurrencySymbol
                ///</summary>
                public static readonly string DefaultCurrencySymbol = GetAppConfigValueFromSiteIfEmpty(Web.SiteDefaultCurrencySymbol, "GTATransferDefaultCurrencySymbol");

                ///<summary>
                /// DefaultSiteCulture
                ///</summary>
                public static readonly string DefaultSiteCulture = GetAppConfigValueFromSiteIfEmpty(Web.SiteCulture, "GTATransferDefaultSiteCulture");

                /// <summary>
                /// Ürün üzerine uygulanacak komisyon oranı 
                /// </summary>
                public static readonly string CommissionPercentage = ConfigurationManager.AppSettings["GTATransferCommissionPercentage"];

                ///<summary>
                /// Loglamanın nereye yapılacağını belirtir
                ///</summary>
                public static readonly string LoggerName = GetAppConfigValueFromSiteIfEmpty(FlightProviders.Log2CategoryName, "GTATransferLogger");

                ///<summary>
                /// Loglamanın yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableLog
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableLog;

                        var isEnable = GetBooleanValue("GTATransferEnableLog");

                        return isEnable && rootEnable;
                    }
                }

                ///<summary>
                /// Validasyonunun yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableValidation
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableValidation;

                        var isEnable = GetBooleanValue("GTATransferEnableValidations");

                        return isEnable && rootEnable;
                    }
                }

                #region Log2MailToList
                /// <summary>
                /// Log2MailToList
                /// </summary>
                public static readonly string Log2MailToList = GetLog2MailToListByProvider(TransferProviders.Log2MailToList, ServiceRegisterName);

                #endregion

                ///<summary>
                /// Servis modu (Test/Prod)
                ///</summary>
                public static bool ServiceModeInTest
                {
                    get
                    {
                        var rootEnable = Web.ServiceModeInTest;

                        var serviceMode = GetBooleanValue(ServiceRegisterName + "ServiceMode");

                        return serviceMode && rootEnable;
                    }
                }

                #region CurrencyToStringFormat
                /// <summary>
                /// Para değerlerinin gösterim formatı
                /// </summary>
                public static readonly string CurrencyToStringFormat = GetAppConfigValueFromSiteIfEmpty(FlightProviders.CurrencyToStringFormat, "GTATransferCurrencyToStringFormat");

                #endregion
            }

        }

        ///<summary>
        /// EventGuide
        ///</summary>
        public sealed class EventGuideProviders
        {
            #region Log2
            ///<summary>
            /// Log2CategoryName
            ///</summary>
            public static readonly string Log2CategoryName = GetAppConfigValueFromSiteIfEmpty(Logging.Log2DbCategoryName, "Log2EventGuideCategoryName");

            #endregion

            #region Is Enable
            ///<summary>
            /// Loglamanın yapılıp yapılamayacağını belirtir
            ///</summary>
            public static bool EnableLog
            {
                get
                {
                    var rootEnable = Logging.EnableLog;

                    var isEnable = GetBooleanValue("EnableEventGuideLog");

                    return isEnable && rootEnable;
                }
            }

            ///<summary>
            /// Validasyonunun yapılıp yapılamayacağını belirtir
            ///</summary>
            public static bool EnableValidation
            {
                get
                {
                    var rootEnable = Validations.EnableValidations;

                    var isEnable = GetBooleanValue("EnableEventGuideValidations");

                    return isEnable && rootEnable;
                }
            }
            #endregion

            #region CurrencyToStringFormat
            /// <summary>
            /// Para değerlerinin gösterim formatı
            /// </summary>
            public static readonly string CurrencyToStringFormat = GetAppConfigValueFromSiteIfEmpty(Web.SiteCurrencyToStringFormat, "EventGuideCurrencyToStringFormat");

            #endregion

            /// <summary>
            /// ModuleName
            /// </summary>
            public static readonly string ModuleName = "EventGuide";//TODO:Db deki module tablosundan almalı

            #region Log2MailToList
            /// <summary>
            /// Log2MailToList
            /// </summary>
            public static readonly string Log2MailToList = ConfigurationManager.AppSettings[ModuleName + "Log2MailToList"];

            /// <summary>
            /// Log2MailFrom
            /// </summary>
            public static readonly string Log2MailFrom = ConfigurationManager.AppSettings[ModuleName + "Log2MailFrom"];

            /// <summary>
            /// MailSubjectKey
            /// </summary>
            public static readonly string MailSubjectKey = ConfigurationManager.AppSettings[ModuleName + "MailSubjectKey"];
            #endregion

            ///<summary>
            /// Servis modu (Test/Prod)
            ///</summary>
            public static bool ServiceModeInTest
            {
                get
                {
                    var rootEnable = Web.ServiceModeInTest;

                    var serviceMode = GetBooleanValue(ModuleName + "ServiceMode");

                    return serviceMode && rootEnable;
                }
            }

            ///<summary>
            /// OnlineTicketEventGuide servisi için ayarlar
            ///</summary>
            public sealed class OnlineTicketEventGuide
            {
                ///<summary>
                /// DI içerisine register edildiği isim
                ///</summary>
                public static readonly string ServiceRegisterName = "OnlineTicketExpress";

                ///<summary>
                /// CountryCode
                ///</summary>
                public static readonly string CountryCode = ConfigurationManager.AppSettings["OnlineTicketExpressCountryCode"];

                ///<summary>
                /// Language
                ///</summary>
                public static readonly string Language = ConfigurationManager.AppSettings["OnlineTicketExpressLang"];

                ///<summary>
                /// ServiceUrl
                ///</summary>
                public static readonly string ServiceUrl = ConfigurationManager.AppSettings["OnlineTicketExpressServiceUrl"];

                ///<summary>
                /// ServiceUserName
                ///</summary>
                public static readonly string ServiceUserName = ConfigurationManager.AppSettings["OnlineTicketExpressServiceUserName"];

                ///<summary>
                /// ServicePassword
                ///</summary>
                public static readonly string ServicePassword = ConfigurationManager.AppSettings["OnlineTicketExpressServicePassword"];

                ///<summary>
                /// ServiceRequestType
                ///</summary>
                public static readonly string ServiceRequestType = ConfigurationManager.AppSettings["OnlineTicketExpressServiceRequestType"];

                ///<summary>
                /// CurrencyConverter
                ///</summary>
                public static readonly string CurrencyConverter = ConfigurationManager.AppSettings["OnlineTicketExpressCurrencyConverter"];

                ///<summary>
                /// DefaultCurrencySymbol
                ///</summary>
                public static readonly string DefaultCurrencySymbol = GetAppConfigValueFromSiteIfEmpty(Web.SiteDefaultCurrencySymbol, "OnlineTicketExpressDefaultCurrencySymbol");

                ///<summary>
                /// DefaultSiteCulture
                ///</summary>
                public static readonly string DefaultSiteCulture = GetAppConfigValueFromSiteIfEmpty(Web.SiteCulture, "OnlineTicketExpressDefaultSiteCulture");

                /// <summary>
                /// Ürün üzerine uygulanacak komisyon oranı 
                /// </summary>
                public static readonly string CommissionPercentage = ConfigurationManager.AppSettings["OnlineTicketExpressCommissionPercentage"];

                ///<summary>
                /// Loglamanın nereye yapılacağını belirtir
                ///</summary>
                public static readonly string LoggerName = GetAppConfigValueFromSiteIfEmpty(EventGuideProviders.Log2CategoryName, "OnlineTicketExpressLogger");

                ///<summary>
                /// Loglamanın yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableLog
                {
                    get
                    {
                        var rootEnable = EventGuideProviders.EnableLog;

                        var isEnable = GetBooleanValue("OnlineTicketExpressEnableLog");

                        return isEnable && rootEnable;
                    }
                }

                ///<summary>
                /// Validasyonunun yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableValidation
                {
                    get
                    {
                        var rootEnable = EventGuideProviders.EnableValidation;

                        var isEnable = GetBooleanValue("OnlineTicketExpressEnableValidations");

                        return isEnable && rootEnable;
                    }
                }

                #region Log2MailToList
                /// <summary>
                /// Log2MailToList
                /// </summary>
                public static readonly string Log2MailToList = GetLog2MailToListByProvider(EventGuideProviders.Log2MailToList, ServiceRegisterName);

                #endregion

                ///<summary>
                /// Servis modu (Test/Prod)
                ///</summary>
                public static bool ServiceModeInTest
                {
                    get
                    {
                        var rootEnable = Web.ServiceModeInTest;

                        var serviceMode = GetBooleanValue(ServiceRegisterName + "ServiceMode");

                        return serviceMode && rootEnable;
                    }
                }

                #region CurrencyToStringFormat
                /// <summary>
                /// Para değerlerinin gösterim formatı
                /// </summary>
                public static readonly string CurrencyToStringFormat = GetAppConfigValueFromSiteIfEmpty(EventGuideProviders.CurrencyToStringFormat, "OnlineTicketExpressCurrencyToStringFormat");

                #endregion
            }

        }

        ///<summary>
        /// Uçuş servisi işlemlerinin static property değerlerine erişimi sağlayan sınıf
        ///</summary>
        public sealed class FlightProviders
        {
            /// <summary>
            /// ModuleName
            /// </summary>
            public static readonly string ModuleName = "Flight";

            #region Log2
            ///<summary>
            /// Log2CategoryName
            ///</summary>
            public static readonly string Log2CategoryName = GetAppConfigValueFromSiteIfEmpty(Logging.Log2DbCategoryName, "Log2FlightCategoryName");

            #endregion

            #region Is
            ///<summary>
            /// Loglamanın yapılıp yapılamayacağını belirtir
            ///</summary>
            public static bool EnableLog
            {
                get
                {
                    var rootEnable = Logging.EnableLog;

                    var isEnable = GetBooleanValue("EnableFlightLog");

                    return isEnable && rootEnable;
                }
            }

            ///<summary>
            /// Validasyonunun yapılıp yapılamayacağını belirtir
            ///</summary>
            public static bool EnableValidation
            {
                get
                {
                    var rootEnable = Validations.EnableValidations;

                    var isEnable = GetBooleanValue("EnableFlightValidations");

                    return isEnable && rootEnable;
                }
            }

            ///<summary>
            /// Servis modu (Test/Prod)
            ///</summary>
            public static bool ServiceModeInTest
            {
                get
                {
                    var rootEnable = Web.ServiceModeInTest;

                    var serviceMode = GetBooleanValue("FlightServiceMode");

                    return serviceMode && rootEnable;
                }
            }
            #endregion

            ///<summary>
            /// 
            ///</summary>
            public static string EticketInfosTokenKey
            {
                get
                {
                    return "EticketInfos";
                }
            }

            #region CurrencyToStringFormat
            /// <summary>
            /// Para değerlerinin gösterim formatı
            /// </summary>
            public static readonly string CurrencyToStringFormat = GetAppConfigValueFromSiteIfEmpty(Web.SiteCurrencyToStringFormat, "FlightCurrencyToStringFormat");

            #endregion

            /// <summary>
            /// CIP Type
            /// </summary>
            public static readonly string CIPTypeName = "CIP";

            /// <summary>
            /// LowCost değeri (Amadeus uçuşlarında Amadeus provider bu ise)
            /// </summary>
            public static readonly string EretailWebFareProvider = "ERetailWebFareProvider";

            /// <summary>
            /// Flight seviyesinde fee alınıp alınmayacağı
            /// </summary>
            public static string SelectedFareItem
            {
                get { return "SelectedFareItem"; }
            }

            /// <summary>
            /// Flight seviyesinde fee alınıp alınmayacağı
            /// </summary>
            public static string OldPrice
            {
                get { return "OldPrice"; }
            }
            public static string OriginalPrice
            {
                get { return "OriginalPrice"; }
            }

            public static string AmadeusPrice
            {
                get { return "AmadeusPrice"; }
            }
            public static string CampaignPrice
            {
                get { return "CampaignPrice"; }
            }

            /// <summary>
            /// Flight seviyesinde fee alınıp alınmayacağı
            /// </summary>
            public static string FareBasisCode
            {
                get { return "FareBasisCode"; }
            }
            public static string GalCommission
            {
                get { return "GalCommission"; }
            }
            public static string WingCommission
            {
                get { return "WingCommission"; }
            }
            public static string MilesGalCommission
            {
                get { return "MilesGalCommission"; }
            }
            public static string MilesWingCommission
            {
                get { return "MilesWingCommission"; }
            }
            public static string MilesPrices
            {
                get { return "MilesPrice"; }
            }
            public static string FareBasisCurreny
            {
                get { return "FareBasisCurreny"; }
            }
            public static string UserDiscount
            {
                get { return " UserDiscount"; }
            }
            public static string Description
            {
                get { return " Description"; }
            }
            public static string Title
            {
                get { return " Title"; }
            }
            public static string TotalMilesPrice
            {
                get { return " TotalMilesPrice"; }
            }
            public static string PercentDiscount
            {
                get { return " PercentDiscount"; }
            }
            public static string MaxUsableCount
            {
                get { return " MaxUsableCount"; }
            }
            public static string CurrentCount
            {
                get { return " CurrentCount"; }
            }
            public static string FareBasisDbId
            {
                get { return " FareBasisDbId"; }
            }
            public static string SalePrice
            {
                get { return " SalePrice"; }
            }
            public static string SelectedPrice
            {
                get { return " SelectedPrice"; }
            }
            public static string FbPercentDiscount
            {
                get { return " FbPercentDiscount"; }
            }
            public static string CampaignPrefix
            {
                get { return "  CampaignPrefix"; }
            }
            public static string MaxuUsableCountParent
            {
                get { return " MaxuUsableCountParent"; }
            }
            public static string PerUserMaxCount
            {
                get { return " PerUserMaxCount"; }
            }


            #region Log2MailToList
            /// <summary>
            /// Log2MailToList
            /// </summary>
            public static readonly string Log2MailToList = ConfigurationManager.AppSettings[ModuleName + "Log2MailToList"];

            /// <summary>
            /// Log2MailFrom
            /// </summary>
            public static readonly string Log2MailFrom = ConfigurationManager.AppSettings[ModuleName + "Log2MailFrom"];

            /// <summary>
            /// MailSubjectKey
            /// </summary>
            public static readonly string MailSubjectKey = ConfigurationManager.AppSettings[ModuleName + "MailSubjectKey"];
            #endregion

            /// <summary>
            /// Flight seviyesinde rezervasyonun yapılıp yapılamayacağı
            /// </summary>
            public static bool EnableReservation
            {
                get
                {
                    var isEnable = GetBooleanValue(ModuleName + "EnableReservation", true);

                    return isEnable;
                }
            }

            /// <summary>
            /// Flight seviyesinde fee alınıp alınmayacağı
            /// </summary>
            public static bool EnableServiceFee
            {
                get
                {
                    var siteEnable = Web.EnableServiceFee;
                    var isEnable = GetBooleanValue(ModuleName + "EnableServiceFee", true);

                    return isEnable && siteEnable;
                }
            }

            /// <summary>
            /// Flight seviyesinde FareBasis alınıp alınmayacağı
            /// </summary>
            public static bool EnableFareBasis
            {
                get
                {
                    var siteEnable = Web.EnableFareBasis;
                    var isEnable = GetBooleanValue(ModuleName + "EnableFareBasis", false);

                    return isEnable && siteEnable;
                }
            }

            ///<summary>
            /// THY uçuş servisi için ayarlar
            ///</summary>
            public class THY
            {
                ///<summary>
                /// Logo
                ///</summary>
                public static readonly string LogoTHY = ConfigurationManager.AppSettings["LogoTHY"];

                ///<summary>
                /// Logo
                ///</summary>
                public static readonly string LogoAnadoluJet = ConfigurationManager.AppSettings["LogoAnadolujet"];

                ///<summary>
                /// DI içerisine register edildiği isim
                ///</summary>
                public static readonly string ServiceRegisterName = "THY";

                ///<summary>
                /// PNR Çıktı yapılabilir
                ///</summary>
                public static bool NeedTicketPrint
                {
                    get
                    {
                        var isEnable = GetBooleanValue("THYNeedTicketPrint");

                        return isEnable;
                    }
                }

                ///<summary>
                /// CorporatePin
                ///</summary>
                public static readonly string CorporatePin = ConfigurationManager.AppSettings["THYCorporatePin"];

                #region Authentication Info
                ///<summary>
                /// UserName
                ///</summary>
                public static readonly string UserName = ConfigurationManager.AppSettings["THYUserName"];

                ///<summary>
                /// Password
                ///</summary>
                public static readonly string Password = ConfigurationManager.AppSettings["THYPassword"];

                ///<summary>
                /// Url
                ///</summary>
                public static readonly string Url = ConfigurationManager.AppSettings["THYUrl"];
                #endregion

                ///<summary>
                /// MarketingAirlineName
                ///</summary>
                public static readonly string AnadolujetMarketingAirlineName = ConfigurationManager.AppSettings["AnadolujetMarketingAirlineName"];

                ///<summary>
                /// MarketingAirlineName
                ///</summary>
                public static readonly string THYMarketingAirlineName = ConfigurationManager.AppSettings["THYMarketingAirlineName"];

                ///<summary>
                /// QueueForTicket
                ///</summary>
                public static readonly string QueueForTicket = ConfigurationManager.AppSettings["QueueForTicket"];

                ///<summary>
                /// QueueForTicket
                ///</summary>
                public static readonly string QueueForReservation = ConfigurationManager.AppSettings["QueueForReservation"];

                ///<summary>
                /// THYCurrencyConverter
                ///</summary>
                public static readonly string CurrencyConverter = ConfigurationManager.AppSettings["THYCurrencyConverter"];

                ///<summary>
                /// DefaultCurrencySymbol
                ///</summary>
                public static readonly string DefaultCurrencySymbol = GetAppConfigValueFromSiteIfEmpty(Web.SiteDefaultCurrencySymbol, "THYDefaultCurrencySymbol");

                ///<summary>
                /// DefaultSiteCulture
                ///</summary>
                public static readonly string DefaultSiteCulture = GetAppConfigValueFromSiteIfEmpty(Web.SiteCulture, "THYDefaultSiteCulture");

                ///<summary>
                /// EnableServiceFee
                ///</summary>
                public static bool EnableServiceFee
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableServiceFee;

                        var enableReservation = GetBooleanValue(ServiceRegisterName + "EnableServiceFee", true);

                        return rootEnable && enableReservation;
                    }
                }

                ///<summary>
                /// Loglamanın nereye yapılacağını belirtir
                ///</summary>
                public static readonly string LoggerName = GetAppConfigValueFromSiteIfEmpty(FlightProviders.Log2CategoryName, "THYLogger");

                ///<summary>
                /// Loglamanın yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableLog
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableLog;

                        var isEnable = GetBooleanValue("THYEnableLog");

                        return isEnable && rootEnable;
                    }
                }

                ///<summary>
                /// Validasyonunun yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableValidation
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableValidation;

                        var isEnable = GetBooleanValue("THYEnableValidations");

                        return isEnable && rootEnable;
                    }
                }

                ///<summary>
                /// Servis modu (Test=true/Prod=false)
                ///</summary>
                public static bool ServiceModeInTest
                {
                    get
                    {
                        var rootEnable = FlightProviders.ServiceModeInTest;

                        var serviceMode = ConfigurationManager.AppSettings["THYFlightServiceMode"];

                        return rootEnable && serviceMode == "Test";
                    }
                }

                ///<summary>
                /// EnableReservation
                ///</summary>
                public static bool EnableReservation
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableReservation;

                        var enableReservation = GetBooleanValue(ServiceRegisterName + "EnableReservation", true);

                        return rootEnable && enableReservation;
                    }
                }

                #region Log2MailToList
                /// <summary>
                /// Log2MailToList
                /// </summary>
                public static readonly string Log2MailToList = GetLog2MailToListByProvider(FlightProviders.Log2MailToList, ServiceRegisterName);

                #endregion

                #region CurrencyToStringFormat
                /// <summary>
                /// Para değerlerinin gösterim formatı
                /// </summary>
                public static readonly string CurrencyToStringFormat = GetAppConfigValueFromSiteIfEmpty(FlightProviders.CurrencyToStringFormat, "THYCurrencyToStringFormat");

                #endregion
            }

            ///<summary>
            /// AtlasJet uçuş servisi için ayarlar
            ///</summary>
            public class Atlas
            {
                ///<summary>
                /// DI içerisine register edildiği isim
                ///</summary>
                public static readonly string ServiceRegisterName = "Atlas";

                ///<summary>
                /// Logo
                ///</summary>
                public static readonly string Logo = ConfigurationManager.AppSettings["LogoAtlas"];

                ///<summary>
                /// MarketingAirlineName
                ///</summary>
                public static readonly string MarketingAirlineName = ConfigurationManager.AppSettings["AtlasMarketingAirlineName"];

                ///<summary>
                /// UserName
                ///</summary>
                public static readonly string UserName = ConfigurationManager.AppSettings["AtlasUserName"];

                ///<summary>
                /// Password
                ///</summary>
                public static readonly string Password = ConfigurationManager.AppSettings["AtlasPassword"];

                ///<summary>
                /// Url
                ///</summary>
                public static readonly string Url = ConfigurationManager.AppSettings["AtlasUrl"];

                ///<summary>
                /// Lang
                ///</summary>
                public static readonly string Lang = ConfigurationManager.AppSettings["AtlasLang"];

                ///<summary>
                /// AtlasCurrencyConverter
                ///</summary>
                public static readonly string CurrencyConverter = ConfigurationManager.AppSettings["AtlasCurrencyConverter"];

                ///<summary>
                /// DefaultCurrencySymbol
                ///</summary>
                public static readonly string DefaultCurrencySymbol = GetAppConfigValueFromSiteIfEmpty(Web.SiteDefaultCurrencySymbol, "AtlasDefaultCurrencySymbol");

                ///<summary>
                /// DefaultSiteCulture
                ///</summary>
                public static readonly string DefaultSiteCulture = GetAppConfigValueFromSiteIfEmpty(Web.SiteCulture, "AtlasDefaultSiteCulture");

                ///<summary>
                /// EnableServiceFee
                ///</summary>
                public static bool EnableServiceFee
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableServiceFee;

                        var enableServiceFee = GetBooleanValue(ServiceRegisterName + "EnableServiceFee", true);

                        return rootEnable && enableServiceFee;
                    }
                }

                ///<summary>
                /// Loglamanın nereye yapılacağını belirtir
                ///</summary>
                public static readonly string LoggerName = GetAppConfigValueFromSiteIfEmpty(FlightProviders.Log2CategoryName, "AtlasLogger");

                ///<summary>
                /// Loglamanın yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableLog
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableLog;

                        var isEnable = GetBooleanValue("AtlasEnableLog");

                        return isEnable && rootEnable;
                    }
                }

                ///<summary>
                /// Validasyonunun yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableValidation
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableValidation;

                        var isEnable = GetBooleanValue("AtlasEnableValidations");

                        return isEnable && rootEnable;
                    }
                }

                ///<summary>
                /// Servis modu (Test/Prod)
                ///</summary>
                public static bool ServiceModeInTest
                {
                    get
                    {
                        var rootEnable = FlightProviders.ServiceModeInTest;

                        var serviceMode = ConfigurationManager.AppSettings["AtlasFlightServiceMode"];

                        return rootEnable && serviceMode == "Test";
                    }
                }

                ///<summary>
                /// EnableReservation
                ///</summary>
                public static bool EnableReservation
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableReservation;

                        var enableReservation = GetBooleanValue(ServiceRegisterName + "EnableReservation", true);

                        return rootEnable && enableReservation;
                    }
                }

                #region Log2MailToList
                /// <summary>
                /// Log2MailToList
                /// </summary>
                public static readonly string Log2MailToList = GetLog2MailToListByProvider(FlightProviders.Log2MailToList, ServiceRegisterName);

                #endregion

                #region CurrencyToStringFormat
                /// <summary>
                /// Para değerlerinin gösterim formatı
                /// </summary>
                public static readonly string CurrencyToStringFormat = GetAppConfigValueFromSiteIfEmpty(FlightProviders.CurrencyToStringFormat, "AtlasCurrencyToStringFormat");

                #endregion
            }

            ///<summary>
            /// Amadeus uçuş servisi için ayarlar
            ///</summary>
            public class Amadeus
            {
                ///<summary>
                /// DI içerisine register edildiği isim
                ///</summary>
                public static readonly string ServiceRegisterName = "Amadeus";

                ///<summary>
                /// Logo
                ///</summary>
                public static readonly string Logo = ConfigurationManager.AppSettings["LogoAmadeus"];

                ///<summary>
                /// UserName
                ///</summary>
                public static readonly string UserName = ConfigurationManager.AppSettings["AmadeusUserName"];

                ///<summary>
                /// Password
                ///</summary>
                public static readonly string Password = ConfigurationManager.AppSettings["AmadeusPassword"];

                ///<summary>
                /// Url
                ///</summary>
                public static readonly string Url = ConfigurationManager.AppSettings["AmadeusUrl"];

                ///<summary>
                /// SpecificFlightInfo
                ///</summary>
                public static string SpecificFlightInfo
                {
                    get
                    {
                        var returnVal = ConfigurationManager.AppSettings["AmadeusSpecificFlightInfo"];

                        return returnVal;
                    }
                }

                ///<summary>
                /// SpecificServiceFeeValues
                ///</summary>
                public static string SpecificServiceFeeValues
                {
                    get
                    {
                        var returnVal = ConfigurationManager.AppSettings["AmadeusSpecificServiceFeeValues"];

                        return returnVal;
                    }
                }

                ///<summary>
                /// EnableSpecificServiceFeeValues
                ///</summary>
                public static bool EnableSpecificServiceFeeValues
                {
                    get
                    {
                        var enableSpecificServiceFeeValues = GetBooleanValue("AmadeusEnableSpecificServiceFeeValues", false);

                        return enableSpecificServiceFeeValues;
                    }
                }

                ///<summary>
                /// SearchTypeByProvider
                ///</summary>
                public static string SearchTypeByProvider
                {
                    get
                    {
                        var returnVal = ConfigurationManager.AppSettings["AmadeusSearchTypeByProvider"];

                        return returnVal;
                    }
                }

                ///<summary>
                /// CorporatePin
                ///</summary>
                public static string CorporatePin
                {
                    get
                    {
                        var returnVal = ConfigurationManager.AppSettings["AmadeusCorporatePin"];

                        return returnVal;
                    }
                }

                ///<summary>
                /// CorporatePin
                ///</summary>
                public static string NegotiatedFareCode
                {
                    get
                    {
                        var returnVal = ConfigurationManager.AppSettings["AmadeusNegotiatedFareCode"];

                        return returnVal;
                    }
                }

                ///<summary>
                /// QueueNumber
                ///</summary>
                public static string QueueNumber
                {
                    get
                    {
                        var returnVal = ConfigurationManager.AppSettings["AmadeusQueueNumber"];

                        return returnVal;
                    }
                }

                ///<summary>
                /// QueueCategory
                ///</summary>
                public static string QueueCategory
                {
                    get
                    {
                        var returnVal = ConfigurationManager.AppSettings["AmadeusQueueCategory"];

                        return returnVal;
                    }
                }

                ///<summary>
                /// BookRestrictionByUrl
                ///</summary>
                public static string BookRestrictionByUrl
                {
                    get
                    {
                        var returnVal = ConfigurationManager.AppSettings["AmadeusBookRestrictionByUrlAllowThis"];

                        return returnVal;
                    }
                }

                ///<summary>
                /// AmadeusCurrencyConverter
                ///</summary>
                public static readonly string CurrencyConverter = ConfigurationManager.AppSettings["AmadeusCurrencyConverter"];

                ///<summary>
                /// DefaultCurrencySymbol
                ///</summary>
                public static readonly string DefaultCurrencySymbol = GetAppConfigValueFromSiteIfEmpty(Web.SiteDefaultCurrencySymbol, "AmadeusDefaultCurrencySymbol");

                ///<summary>
                /// DefaultSiteCulture
                ///</summary>
                public static readonly string DefaultSiteCulture = GetAppConfigValueFromSiteIfEmpty(Web.SiteCulture, "AmadeusDefaultSiteCulture");

                ///<summary>
                /// Yüzde komisyon
                ///</summary>
                public static readonly string CommissionPercentage = ConfigurationManager.AppSettings["AmadeusCommissionPercentage"];

                ///<summary>
                /// EnableServiceFee
                ///</summary>
                public static bool EnableServiceFee
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableServiceFee;

                        var enableServiceFee = GetBooleanValue(ServiceRegisterName + "EnableServiceFee", true);

                        return rootEnable && enableServiceFee;
                    }
                }

                /// <summary>
                /// Flight seviyesinde FareBasis alınıp alınmayacağı
                /// </summary>
                public static bool EnableFareBasis
                {
                    get
                    {
                        var siteEnable = FlightProviders.EnableFareBasis;
                        var isEnable = GetBooleanValue(ServiceRegisterName + "EnableFareBasis", false);

                        return isEnable && siteEnable;
                    }
                }

                ///<summary>
                /// Amadeus Session Alive Time Limit
                ///</summary>
                public static readonly string SessionTimeOutLimitAsSecond = ConfigurationManager.AppSettings["AmadeusSessionTimeOutLimitAsSecond"];

                ///<summary>
                /// Loglamanın nereye yapılacağını belirtir
                ///</summary>
                public static readonly string LoggerName = GetAppConfigValueFromSiteIfEmpty(FlightProviders.Log2CategoryName, "AmadeusLogger");

                ///<summary>
                /// Loglamanın yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableLog
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableLog;

                        var isEnable = GetBooleanValue("AmadeusEnableLog");

                        return isEnable && rootEnable;
                    }
                }

                ///<summary>
                /// Validasyonunun yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableValidation
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableValidation;

                        var isEnable = GetBooleanValue("AmadeusEnableValidations");

                        return isEnable && rootEnable;
                    }
                }

                ///<summary>
                /// Servis modu (Test/Prod)
                ///</summary>
                public static bool ServiceModeInTest
                {
                    get
                    {
                        var rootEnable = FlightProviders.ServiceModeInTest;

                        var serviceMode = ConfigurationManager.AppSettings["AmadeusFlightServiceMode"];

                        return rootEnable && serviceMode == "Test";
                    }
                }

                ///<summary>
                /// EnableReservation
                ///</summary>
                public static bool EnableReservation
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableReservation;

                        var enableReservation = GetBooleanValue(ServiceRegisterName + "EnableReservation", true);

                        return rootEnable && enableReservation;
                    }
                }

                #region Log2MailToList

                /// <summary>
                /// Log2MailToList
                /// </summary>
                public static readonly string Log2MailToList = GetLog2MailToListByProvider(FlightProviders.Log2MailToList, ServiceRegisterName);

                #endregion

                #region CurrencyToStringFormat
                /// <summary>
                /// Para değerlerinin gösterim formatı
                /// </summary>
                public static readonly string CurrencyToStringFormat = GetAppConfigValueFromSiteIfEmpty(FlightProviders.CurrencyToStringFormat, "AmadeusCurrencyToStringFormat");

                #endregion
            }

            ///<summary>
            /// AmadeusPegasus uçuş servisi için ayarlar
            ///</summary>
            public class AmadeusPegasus
            {
                ///<summary>
                /// DI içerisine register edildiği isim
                ///</summary>
                public static readonly string ServiceRegisterName = "AmadeusPegasus";

                ///<summary>
                /// Logo
                ///</summary>
                public static readonly string Logo = ConfigurationManager.AppSettings["LogoAmadeusPegasus"];

                ///<summary>
                /// UserName
                ///</summary>
                public static readonly string UserName = ConfigurationManager.AppSettings["AmadeusPegasusUserName"];

                ///<summary>
                /// Password
                ///</summary>
                public static readonly string Password = ConfigurationManager.AppSettings["AmadeusPegasusPassword"];

                ///<summary>
                /// Url
                ///</summary>
                public static readonly string Url = ConfigurationManager.AppSettings["AmadeusPegasusUrl"];

                ///<summary>
                /// SpecificFlightInfo
                ///</summary>
                public static string SpecificFlightInfo
                {
                    get
                    {
                        var returnVal = ConfigurationManager.AppSettings["AmadeusPegasusSpecificFlightInfo"];

                        return returnVal;
                    }
                }

                ///<summary>
                /// CorporatePin
                ///</summary>
                public static string CorporatePin
                {
                    get
                    {
                        var returnVal = ConfigurationManager.AppSettings["AmadeusPegasusCorporatePin"];

                        return returnVal;
                    }
                }

                ///<summary>
                /// AmadeusPegasusCurrencyConverter
                ///</summary>
                public static readonly string CurrencyConverter = ConfigurationManager.AppSettings["AmadeusPegasusCurrencyConverter"];

                ///<summary>
                /// DefaultCurrencySymbol
                ///</summary>
                public static readonly string DefaultCurrencySymbol = GetAppConfigValueFromSiteIfEmpty(Web.SiteDefaultCurrencySymbol, "AmadeusPegasusDefaultCurrencySymbol");

                ///<summary>
                /// DefaultSiteCulture
                ///</summary>
                public static readonly string DefaultSiteCulture = GetAppConfigValueFromSiteIfEmpty(Web.SiteCulture, "AmadeusPegasusDefaultSiteCulture");

                ///<summary>
                /// Yüzde komisyon
                ///</summary>
                public static readonly string CommissionPercentage = ConfigurationManager.AppSettings["AmadeusPegasusCommissionPercentage"];

                ///<summary>
                /// EnableServiceFee
                ///</summary>
                public static bool EnableServiceFee
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableServiceFee;

                        var enableServiceFee = GetBooleanValue(ServiceRegisterName + "EnableServiceFee", true);

                        return rootEnable && enableServiceFee;
                    }
                }

                ///<summary>
                /// AmadeusPegasus Session Alive Time Limit
                ///</summary>
                public static readonly string SessionTimeOutLimitAsSecond = ConfigurationManager.AppSettings["AmadeusPegasusSessionTimeOutLimitAsSecond"];

                ///<summary>
                /// Loglamanın nereye yapılacağını belirtir
                ///</summary>
                public static readonly string LoggerName = GetAppConfigValueFromSiteIfEmpty(FlightProviders.Log2CategoryName, "AmadeusPegasusLogger");

                ///<summary>
                /// Loglamanın yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableLog
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableLog;

                        var isEnable = GetBooleanValue("AmadeusPegasusEnableLog");

                        return isEnable && rootEnable;
                    }
                }

                ///<summary>
                /// Validasyonunun yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableValidation
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableValidation;

                        var isEnable = GetBooleanValue("AmadeusPegasusEnableValidations");

                        return isEnable && rootEnable;
                    }
                }

                ///<summary>
                /// Servis modu (Test/Prod)
                ///</summary>
                public static bool ServiceModeInTest
                {
                    get
                    {
                        var rootEnable = FlightProviders.ServiceModeInTest;

                        var serviceMode = ConfigurationManager.AppSettings["AmadeusPegasusFlightServiceMode"];

                        return rootEnable && serviceMode == "Test";
                    }
                }

                ///<summary>
                /// EnableReservation
                ///</summary>
                public static bool EnableReservation
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableReservation;

                        var enableReservation = GetBooleanValue(ServiceRegisterName + "EnableReservation", true);

                        return rootEnable && enableReservation;
                    }
                }

                #region Log2MailToList

                /// <summary>
                /// Log2MailToList
                /// </summary>
                public static readonly string Log2MailToList = GetLog2MailToListByProvider(FlightProviders.Log2MailToList, ServiceRegisterName);

                #endregion

                #region CurrencyToStringFormat
                /// <summary>
                /// Para değerlerinin gösterim formatı
                /// </summary>
                public static readonly string CurrencyToStringFormat = GetAppConfigValueFromSiteIfEmpty(FlightProviders.CurrencyToStringFormat, "AmadeusPegasusCurrencyToStringFormat");

                #endregion
            }

            ///<summary>
            /// SunExpress uçuş servisi için ayarlar
            ///</summary>
            public class SunExpress
            {
                ///<summary>
                /// DI içerisine register edildiği isim
                ///</summary>
                public static readonly string ServiceRegisterName = "SunExpress";

                ///<summary>
                /// Logo
                ///</summary>
                public static readonly string Logo = ConfigurationManager.AppSettings["LogoSunExpress"];

                ///<summary>
                /// UserName
                ///</summary>
                public static readonly string UserName = ConfigurationManager.AppSettings["SunExpressUserName"];

                ///<summary>
                /// Password
                ///</summary>
                public static readonly string Password = ConfigurationManager.AppSettings["SunExpressPassword"];

                ///<summary>
                /// Url
                ///</summary>
                public static readonly string Url = ConfigurationManager.AppSettings["SunExpressUrl"];

                ///<summary>
                /// MarketingAirlineName
                ///</summary>
                public static readonly string MarketingAirlineName = ConfigurationManager.AppSettings["SunExpressMarketingAirlineName"];


                ///<summary>
                /// SunExpressCurrencyConverter
                ///</summary>
                public static readonly string CurrencyConverter = ConfigurationManager.AppSettings["SunExpressCurrencyConverter"];

                ///<summary>
                /// DefaultCurrencySymbol
                ///</summary>
                public static readonly string DefaultCurrencySymbol = GetAppConfigValueFromSiteIfEmpty(Web.SiteDefaultCurrencySymbol, "SunExpressDefaultCurrencySymbol");

                ///<summary>
                /// DefaultSiteCulture
                ///</summary>
                public static readonly string DefaultSiteCulture = GetAppConfigValueFromSiteIfEmpty(Web.SiteCulture, "SunExpressDefaultSiteCulture");

                ///<summary>
                /// SpecialServiceRequestType sınıfının Text prop'unu set etmek için kullanılan değer. Bu payment type BILL olduğunda kullanılıyor
                ///</summary>
                public static readonly string ServiceRequestBILLTypeText = ConfigurationManager.AppSettings["SunExpressServiceRequestBILLTypeText"];

                ///<summary>
                /// EnableServiceFee
                ///</summary>
                public static bool EnableServiceFee
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableServiceFee;

                        var enableServiceFee = GetBooleanValue(ServiceRegisterName + "EnableServiceFee", true);

                        return rootEnable && enableServiceFee;
                    }
                }

                ///<summary>
                /// Loglamanın nereye yapılacağını belirtir
                ///</summary>
                public static readonly string LoggerName = GetAppConfigValueFromSiteIfEmpty(FlightProviders.Log2CategoryName, "SunExpressLogger");

                ///<summary>
                /// Loglamanın yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableLog
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableLog;

                        var isEnable = GetBooleanValue("SunExpressEnableLog");

                        return isEnable && rootEnable;
                    }
                }

                ///<summary>
                /// Validasyonunun yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableValidation
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableValidation;

                        var isEnable = GetBooleanValue("SunExpressEnableValidations");

                        return isEnable && rootEnable;
                    }
                }

                ///<summary>
                /// Servis modu (Test/Prod)
                ///</summary>
                public static bool ServiceModeInTest
                {
                    get
                    {
                        var rootEnable = FlightProviders.ServiceModeInTest;

                        var serviceMode = ConfigurationManager.AppSettings["SunExpressFlightServiceMode"];

                        return rootEnable && serviceMode == "Test";
                    }
                }

                ///<summary>
                /// EnableReservation
                ///</summary>
                public static bool EnableReservation
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableReservation;

                        var enableReservation = GetBooleanValue(ServiceRegisterName + "EnableReservation", true);

                        return rootEnable && enableReservation;
                    }
                }

                #region Log2MailToList
                /// <summary>
                /// Log2MailToList
                /// </summary>
                public static readonly string Log2MailToList = GetLog2MailToListByProvider(FlightProviders.Log2MailToList, ServiceRegisterName);

                #endregion

                #region CurrencyToStringFormat
                /// <summary>
                /// Para değerlerinin gösterim formatı
                /// </summary>
                public static readonly string CurrencyToStringFormat = GetAppConfigValueFromSiteIfEmpty(FlightProviders.CurrencyToStringFormat, "SunExpressCurrencyToStringFormat");

                #endregion
            }

            ///<summary>
            /// SkyAirlines uçuş servisi için ayarlar
            ///</summary>
            public class SkyAirlines
            {
                ///<summary>
                /// DI içerisine register edildiği isim
                ///</summary>
                public static readonly string ServiceRegisterName = "SkyAirlines";

                ///<summary>
                /// Logo
                ///</summary>
                public static readonly string Logo = ConfigurationManager.AppSettings["LogoSkyAirlines"];

                ///<summary>
                /// UserName
                ///</summary>
                public static readonly string UserName = ConfigurationManager.AppSettings["SkyAirlinesUserName"];

                ///<summary>
                /// Password
                ///</summary>
                public static readonly string Password = ConfigurationManager.AppSettings["SkyAirlinesPassword"];

                ///<summary>
                /// Url
                ///</summary>
                public static readonly string Url = ConfigurationManager.AppSettings["SkyAirlinesUrl"];

                ///<summary>
                /// MarketingAirlineName
                ///</summary>
                public static readonly string MarketingAirlineName = ConfigurationManager.AppSettings["SkyAirlinesMarketingAirlineName"];


                ///<summary>
                /// SkyAirlinesCurrencyConverter
                ///</summary>
                public static readonly string CurrencyConverter = ConfigurationManager.AppSettings["SkyAirlinesCurrencyConverter"];

                ///<summary>
                /// DefaultCurrencySymbol
                ///</summary>
                public static readonly string DefaultCurrencySymbol = GetAppConfigValueFromSiteIfEmpty(Web.SiteDefaultCurrencySymbol, "SkyAirlinesDefaultCurrencySymbol");

                ///<summary>
                /// DefaultSiteCulture
                ///</summary>
                public static readonly string DefaultSiteCulture = GetAppConfigValueFromSiteIfEmpty(Web.SiteCulture, "SkyAirlinesDefaultSiteCulture");

                ///<summary>
                /// SpecialServiceRequestType sınıfının Text prop'unu set etmek için kullanılan değer. Bu payment type BILL olduğunda kullanılıyor
                ///</summary>
                public static readonly string ServiceRequestBILLTypeText = ConfigurationManager.AppSettings["SkyAirlinesServiceRequestBILLTypeText"];

                ///<summary>
                /// EnableServiceFee
                ///</summary>
                public static bool EnableServiceFee
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableServiceFee;

                        var enableServiceFee = GetBooleanValue(ServiceRegisterName + "EnableServiceFee", true);

                        return rootEnable && enableServiceFee;
                    }
                }

                ///<summary>
                /// Loglamanın nereye yapılacağını belirtir
                ///</summary>
                public static readonly string LoggerName = GetAppConfigValueFromSiteIfEmpty(FlightProviders.Log2CategoryName, "SkyAirlinesLogger");

                ///<summary>
                /// Loglamanın yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableLog
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableLog;

                        var isEnable = GetBooleanValue("SkyAirlinesEnableLog");

                        return isEnable && rootEnable;
                    }
                }

                ///<summary>
                /// Validasyonunun yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableValidation
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableValidation;

                        var isEnable = GetBooleanValue("SkyAirlinesEnableValidations");

                        return isEnable && rootEnable;
                    }
                }

                ///<summary>
                /// Servis modu (Test/Prod)
                ///</summary>
                public static bool ServiceModeInTest
                {
                    get
                    {
                        var rootEnable = FlightProviders.ServiceModeInTest;

                        var serviceMode = ConfigurationManager.AppSettings["SkyAirlinesFlightServiceMode"];

                        return rootEnable && serviceMode == "Test";
                    }
                }

                ///<summary>
                /// EnableReservation
                ///</summary>
                public static bool EnableReservation
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableReservation;

                        var enableReservation = GetBooleanValue(ServiceRegisterName + "EnableReservation", true);

                        return rootEnable && enableReservation;
                    }
                }

                #region Log2MailToList
                /// <summary>
                /// Log2MailToList
                /// </summary>
                public static readonly string Log2MailToList = GetLog2MailToListByProvider(FlightProviders.Log2MailToList, ServiceRegisterName);

                #endregion

                #region CurrencyToStringFormat
                /// <summary>
                /// Para değerlerinin gösterim formatı
                /// </summary>
                public static readonly string CurrencyToStringFormat = GetAppConfigValueFromSiteIfEmpty(FlightProviders.CurrencyToStringFormat, "SkyAirlinesCurrencyToStringFormat");

                #endregion
            }

            ///<summary>
            /// BoraJet uçuş servisi
            ///</summary>
            public class BoraJet
            {
                ///<summary>
                /// Logo
                ///</summary>
                public static readonly string LogoBoraJet = ConfigurationManager.AppSettings["LogoBoraJet"];

                ///<summary>
                /// DI içerisine register edildiği isim
                ///</summary>
                public static readonly string ServiceRegisterName = "BoraJet";

                ///<summary>
                /// UserName
                ///</summary>
                public static readonly string UserName = ConfigurationManager.AppSettings["BoraJetUserName"];

                ///<summary>
                /// Password
                ///</summary>
                public static readonly string Password = ConfigurationManager.AppSettings["BoraJetPassword"];

                ///<summary>
                /// Url
                ///</summary>
                public static readonly string Url = ConfigurationManager.AppSettings["BoraJetUrl"];


                ///<summary>
                /// MarketingAirlineName
                ///</summary>
                public static readonly string BoraJetMarketingAirlineName = ConfigurationManager.AppSettings["BoraJetMarketingAirlineName"];

                ///<summary>
                /// BoraJetCurrencyConverter
                ///</summary>
                public static readonly string CurrencyConverter = ConfigurationManager.AppSettings["BoraJetCurrencyConverter"];

                ///<summary>
                /// DefaultCurrencySymbol
                ///</summary>
                public static readonly string DefaultCurrencySymbol = GetAppConfigValueFromSiteIfEmpty(Web.SiteDefaultCurrencySymbol, "BoraJetDefaultCurrencySymbol");

                ///<summary>
                /// DefaultSiteCulture
                ///</summary>
                public static readonly string DefaultSiteCulture = GetAppConfigValueFromSiteIfEmpty(Web.SiteCulture, "BoraJetDefaultSiteCulture");

                ///<summary>
                /// EnableServiceFee
                ///</summary>
                public static bool EnableServiceFee
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableServiceFee;

                        var enableServiceFee = GetBooleanValue(ServiceRegisterName + "EnableServiceFee", true);

                        return rootEnable && enableServiceFee;
                    }
                }

                ///<summary>
                /// Loglamanın nereye yapılacağını belirtir
                ///</summary>
                public static readonly string LoggerName = GetAppConfigValueFromSiteIfEmpty(FlightProviders.Log2CategoryName, "BoraJetLogger");

                ///<summary>
                /// Loglamanın yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableLog
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableLog;

                        var isEnable = GetBooleanValue("BoraJetEnableLog");

                        return isEnable && rootEnable;
                    }
                }

                ///<summary>
                /// Validasyonunun yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableValidation
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableValidation;

                        var isEnable = GetBooleanValue("BoraJetEnableValidations");

                        return isEnable && rootEnable;
                    }
                }

                ///<summary>
                /// Servis modu (Test/Prod)
                ///</summary>
                public static bool ServiceModeInTest
                {
                    get
                    {
                        var rootEnable = FlightProviders.ServiceModeInTest;

                        var serviceMode = ConfigurationManager.AppSettings["BoraJetFlightServiceMode"];

                        return rootEnable && serviceMode == "Test";
                    }
                }

                ///<summary>
                /// EnableReservation
                ///</summary>
                public static bool EnableReservation
                {
                    get
                    {
                        var rootEnable = FlightProviders.EnableReservation;

                        var enableReservation = GetBooleanValue(ServiceRegisterName + "EnableReservation", true);

                        return rootEnable && enableReservation;
                    }
                }

                #region Log2MailToList
                /// <summary>
                /// Log2MailToList
                /// </summary>
                public static readonly string Log2MailToList = GetLog2MailToListByProvider(FlightProviders.Log2MailToList, ServiceRegisterName);

                #endregion

                #region CurrencyToStringFormat
                /// <summary>
                /// Para değerlerinin gösterim formatı
                /// </summary>
                public static readonly string CurrencyToStringFormat = GetAppConfigValueFromSiteIfEmpty(FlightProviders.CurrencyToStringFormat, "BoraJetCurrencyToStringFormat");

                #endregion
            }
        }

        ///<summary>
        /// Araç servisi işlemlerinin static property değerlerine erişimi sağlayan sınıf
        ///</summary>
        public sealed class CarRentalProviders
        {
            #region Log2
            ///<summary>
            /// Log2CategoryName
            ///</summary>
            public static readonly string Log2CategoryName = GetAppConfigValueFromSiteIfEmpty(Logging.Log2DbCategoryName, "Log2CarRentalCategoryName");

            #endregion

            #region Is Enable
            ///<summary>
            /// Loglamanın yapılıp yapılamayacağını belirtir
            ///</summary>
            public static bool EnableLog
            {
                get
                {
                    var rootEnable = Logging.EnableLog;

                    var isEnable = GetBooleanValue("EnableCarRentalLog");

                    return isEnable && rootEnable;
                }
            }

            ///<summary>
            /// Validasyonunun yapılıp yapılamayacağını belirtir
            ///</summary>
            public static bool EnableValidation
            {
                get
                {
                    var rootEnable = Validations.EnableValidations;

                    var isEnable = GetBooleanValue("EnableCarRentalValidations");

                    return isEnable && rootEnable;
                }
            }
            #endregion

            #region CurrencyToStringFormat
            /// <summary>
            /// Para değerlerinin gösterim formatı
            /// </summary>
            public static readonly string CurrencyToStringFormat = GetAppConfigValueFromSiteIfEmpty(Web.SiteCurrencyToStringFormat, "CarRentalCurrencyToStringFormat");

            #endregion

            /// <summary>
            /// ModuleName
            /// </summary>
            public static readonly string ModuleName = "CarRental";//TODO:Db deki module tablosundan almalı

            #region Log2MailToList
            /// <summary>
            /// Log2MailToList
            /// </summary>
            public static readonly string Log2MailToList = ConfigurationManager.AppSettings[ModuleName + "Log2MailToList"];

            /// <summary>
            /// Log2MailFrom
            /// </summary>
            public static readonly string Log2MailFrom = ConfigurationManager.AppSettings[ModuleName + "Log2MailFrom"];

            /// <summary>
            /// MailSubjectKey
            /// </summary>
            public static readonly string MailSubjectKey = ConfigurationManager.AppSettings[ModuleName + "MailSubjectKey"];
            #endregion

            ///<summary>
            /// Servis modu (Test/Prod)
            ///</summary>
            public static bool ServiceModeInTest
            {
                get
                {
                    var rootEnable = Web.ServiceModeInTest;

                    var serviceMode = GetBooleanValue(ModuleName + "ServiceMode");

                    return serviceMode && rootEnable;
                }
            }

            ///<summary>
            /// SixtCarRentalTurkey servisi için ayarlar
            ///</summary>
            public sealed class SixtCarRental
            {
                ///<summary>
                /// DI içerisine register edildiği isim
                ///</summary>
                public static readonly string ServiceRegisterName = "Sixt";

                ///<summary>
                /// CustomerName
                ///</summary>
                public static readonly string CustomerName = ConfigurationManager.AppSettings["SixtCarRentalServiceCustomerName"];

                ///<summary>
                ///</summary>
                public static class CustomerId
                {
                    ///<summary>
                    ///</summary>
                    public static readonly string CorporateDiscountNo = ConfigurationManager.AppSettings["SixtCarRentalCostomerCorporateDiscountNo"];
                }

                ///<summary>
                /// AgentNo
                ///</summary>
                public static readonly string AgentNo = ConfigurationManager.AppSettings["SixtCarRentalAgentNo"];


                ///<summary>
                /// InternalUse
                ///</summary>
                public static readonly string InternalUse = ConfigurationManager.AppSettings["SixtCarRentalServiceInternalUse"];

                ///<summary>
                /// Specification
                ///</summary>
                public static readonly string Specification = ConfigurationManager.AppSettings["SixtCarRentalServiceSpecification"];

                ///<summary>
                /// UserName
                ///</summary>
                public static readonly string UserName = ConfigurationManager.AppSettings["SixtCarRentalUserName"];

                ///<summary>
                /// Password
                ///</summary>
                public static readonly string Password = ConfigurationManager.AppSettings["SixtCarRentalPassword"];

                ///<summary>
                /// Url
                ///</summary>
                public static readonly string Url = ConfigurationManager.AppSettings["SixtCarRentalUrl"];

                ///<summary>
                /// Lang
                ///</summary>
                public static readonly string Lang = ConfigurationManager.AppSettings["SixtCarRentalLang"];

                ///<summary>
                /// POS
                ///</summary>
                public static readonly string POS = ConfigurationManager.AppSettings["SixtCarRentalPOS"];

                ///<summary>
                /// IataNo
                ///</summary>
                public static readonly string IataNo = ConfigurationManager.AppSettings["SixtCarRentalIataNo"];

                ///<summary>
                /// IataNo
                ///</summary>
                public static readonly string RateOfServiceCharge = ConfigurationManager.AppSettings["SixtRateOfServiceCharge"];

                ///<summary>
                ///AuthenticationType
                ///</summary>
                public static readonly string AuthType = "Basic";

                ///<summary>
                /// CurrencyConverter
                ///</summary>
                public static readonly string CurrencyConverter = ConfigurationManager.AppSettings["SixtCarRentalCurrencyConverter"];

                ///<summary>
                /// DefaultCurrencySymbol
                ///</summary>
                public static readonly string DefaultCurrencySymbol = GetAppConfigValueFromSiteIfEmpty(Web.SiteDefaultCurrencySymbol, "SixtCarRentalDefaultCurrencySymbol");

                ///<summary>
                /// DefaultSiteCulture
                ///</summary>
                public static readonly string DefaultSiteCulture = GetAppConfigValueFromSiteIfEmpty(Web.SiteCulture, "SixtCarRentalDefaultSiteCulture");

                ///<summary>
                /// Loglamanın nereye yapılacağını belirtir
                ///</summary>
                public static readonly string LoggerName = GetAppConfigValueFromSiteIfEmpty(CarRentalProviders.Log2CategoryName, "SixtCarRentalLogger");

                ///<summary>
                /// Loglamanın yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableLog
                {
                    get
                    {
                        var rootEnable = CarRentalProviders.EnableLog;

                        var isEnable = GetBooleanValue("SixtCarRentalEnableLog");

                        return isEnable && rootEnable;
                    }
                }

                ///<summary>
                /// Validasyonunun yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableValidation
                {
                    get
                    {
                        var rootEnable = CarRentalProviders.EnableValidation;

                        var isEnable = GetBooleanValue("SixtCarRentalEnableValidations");

                        return isEnable && rootEnable;
                    }
                }

                #region Log2MailToList
                /// <summary>
                /// Log2MailToList
                /// </summary>
                public static readonly string Log2MailToList = GetLog2MailToListByProvider(CarRentalProviders.Log2MailToList, ServiceRegisterName);

                #endregion

                ///<summary>
                /// Servis modu (Test/Prod)
                ///</summary>
                public static bool ServiceModeInTest
                {
                    get
                    {
                        var rootEnable = Web.ServiceModeInTest;

                        var serviceMode = GetBooleanValue(ServiceRegisterName + "ServiceMode");

                        return serviceMode && rootEnable;
                    }
                }

                #region CurrencyToStringFormat
                /// <summary>
                /// Para değerlerinin gösterim formatı
                /// </summary>
                public static readonly string CurrencyToStringFormat = GetAppConfigValueFromSiteIfEmpty(CarRentalProviders.CurrencyToStringFormat, "SixtCarRentalCurrencyToStringFormat");

                #endregion
            }

            ///<summary>
            /// SixtCarRentalTurkeyTurkey servisi için ayarlar
            ///</summary>
            public sealed class SixtTurkeyCarRental
            {
                ///<summary>
                /// DI içerisine register edildiği isim
                ///</summary>
                public static readonly string ServiceRegisterName = "SixtTurkey";

                ///<summary>
                /// CustomerName
                ///</summary>
                public static readonly string UserName = ConfigurationManager.AppSettings["SixtCarRentalTurkeyServicePartnerId"];

                ///<summary>
                ///</summary>
                public static readonly string Password = ConfigurationManager.AppSettings["SixtCarRentalTurkeyServicePassword"];

                ///<summary>
                /// ServiceUrl
                ///</summary>
                public static readonly string ServiceUrl = ConfigurationManager.AppSettings["SixtCarRentalTurkeyServiceUrl"];

                ///<summary>
                //AvailibilityURL
                ///</summary>
                public static readonly string AvailibilityUrl = ConfigurationManager.AppSettings["SixtCarRentalTurkeyServiceAvailibilityURL"];

                ///<summary>
                /// Lang
                ///</summary>
                public static readonly string Lang = ConfigurationManager.AppSettings["SixtCarRentalTurkeyLang"];

                ///<summary>
                // RateOfServiceCharge
                ///</summary>
                public static readonly string RateOfServiceCharge = ConfigurationManager.AppSettings["SixtCarRentalTurkeyRateOfServiceCharge"];

                ///<summary>
                ///AuthenticationType
                ///</summary>
                public static readonly string AuthType = "Basic";

                ///<summary>
                /// CurrencyConverter
                ///</summary>
                public static readonly string CurrencyConverter = ConfigurationManager.AppSettings["SixtCarRentalTurkeyCurrencyConverter"];

                ///<summary>
                /// DefaultCurrencySymbol
                ///</summary>
                public static readonly string DefaultCurrencySymbol = GetAppConfigValueFromSiteIfEmpty(Web.SiteDefaultCurrencySymbol, "SixtCarRentalTurkeyDefaultCurrencySymbol");

                ///<summary>
                /// DefaultSiteCulture
                ///</summary>
                public static readonly string DefaultSiteCulture = GetAppConfigValueFromSiteIfEmpty(Web.SiteCulture, "SixtCarRentalTurkeyDefaultSiteCulture");

                ///<summary>
                /// Loglamanın nereye yapılacağını belirtir
                ///</summary>
                public static readonly string LoggerName = GetAppConfigValueFromSiteIfEmpty(CarRentalProviders.Log2CategoryName, "SixtCarRentalTurkeyLogger");

                ///<summary>
                /// Loglamanın yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableLog
                {
                    get
                    {
                        var rootEnable = CarRentalProviders.EnableLog;

                        var isEnable = GetBooleanValue("SixtCarRentalTurkeyEnableLog");

                        return isEnable && rootEnable;
                    }
                }

                ///<summary>
                /// Validasyonunun yapılıp yapılamayacağını belirtir
                ///</summary>
                public static bool EnableValidation
                {
                    get
                    {
                        var rootEnable = CarRentalProviders.EnableValidation;

                        var isEnable = GetBooleanValue("SixtCarRentalTurkeyEnableValidations");

                        return isEnable && rootEnable;
                    }
                }

                #region Log2MailToList
                /// <summary>
                /// Log2MailToList
                /// </summary>
                public static readonly string Log2MailToList = GetLog2MailToListByProvider(CarRentalProviders.Log2MailToList, ServiceRegisterName);

                #endregion

                ///<summary>
                /// Servis modu (Test/Prod)
                ///</summary>
                public static bool ServiceModeInTest
                {
                    get
                    {
                        var rootEnable = Web.ServiceModeInTest;

                        var serviceMode = GetBooleanValue(ServiceRegisterName + "ServiceMode");

                        return serviceMode && rootEnable;
                    }
                }

                #region CurrencyToStringFormat
                /// <summary>
                /// Para değerlerinin gösterim formatı
                /// </summary>
                public static readonly string CurrencyToStringFormat = GetAppConfigValueFromSiteIfEmpty(CarRentalProviders.CurrencyToStringFormat, "SixtCarRentalTurkeyCurrencyToStringFormat");

                #endregion
            }
        }

        ///<summary>
        /// TripAdvisor Configuration
        ///</summary>
        public sealed class TripAdvisor
        {
            ///<summary>
            /// TripAdvisorPartnerId
            ///</summary>
            public static readonly string PartnerId = ConfigurationManager.AppSettings["TripAdvisorPartnerId"];
        }

        ///<summary>
        /// Log işlemlerinin static property değerlerine erişimi sağlayan sınıf
        ///</summary>
        public sealed class Promotions
        {
            ///<summary>
            /// Kampanya servis 
            ///</summary>
            public static bool EnableCampaign
            {
                get
                {
                    var isEnable = GetBooleanValue("EnableCampaign");

                    return isEnable;
                }
            }

            ///<summary>
            /// Kampanya servis 
            ///</summary>
            public static bool EnablePromotion
            {
                get
                {
                    var isEnable = GetBooleanValue("PromotionCouponActive");

                    return isEnable;
                }
            }
        }

        ///<summary>
        /// Log işlemlerinin static property değerlerine erişimi sağlayan sınıf
        ///</summary>
        public sealed class Logging
        {
            ///<summary>
            /// Log2DbCategoryName
            ///</summary>
            public static readonly string Log2DbCategoryName = ConfigurationManager.AppSettings[Log2DbCategoryNameKey];

            ///<summary>
            /// Presentasyon tarafında kullanılacak log kategorisi
            ///</summary>
            public static readonly string Log2Db4Web = GetAppConfigValueFromSiteIfEmpty(Logging.Log2DbCategoryName, "Log2Db4Web");

            ///<summary>
            /// Presentasyon tarafında template oluşturulma işlemi için log kategorisi
            ///</summary>
            public static readonly string Log2Db4WebTemplate = GetAppConfigValueFromSiteIfEmpty(Logging.Log2DbCategoryName, "Log2Db4WebTemplate");

            ///<summary>
            /// Döviz kuru işlemleri sırasında loglama yapacak logger
            ///</summary>
            public static readonly string Log2ExchangeRate = GetAppConfigValueFromSiteIfEmpty(Logging.Log2DbCategoryName, "Log2ExchangeRate");

            ///<summary>
            /// Database erişim tarafında kullanılacak log kategorisi
            ///</summary>
            public static readonly string Log2Db4DAL = ConfigurationManager.AppSettings["Log2Db4DAL"];

            ///<summary>
            /// Database erişim tarafında kullanılacak log kategorisi
            ///</summary>
            public static readonly string Log2EMailCategoryName = "SendMail";

            ///<summary>
            /// EnableLog
            ///</summary>
            public static bool EnableLog
            {
                get
                {
                    var isEnable = GetBooleanValue(EnableLogKey);

                    return isEnable;
                }
            }

            ///<summary>
            /// Presentasyon tarafında template oluşturulma işlemi sırasında loglama yapılıp yapılamayacağını belirtir
            ///</summary>
            public static bool EnableLog2Db4WebTemplate
            {
                get
                {
                    var isEnable = GetBooleanValue("EnableLog2Db4WebTemplate");

                    return isEnable;
                }
            }

            ///<summary>
            /// Servislerdeki Core sınıfların validasyonu sırasında loglama yapılıp yapılamayacağını belirtir
            ///</summary>
            public static bool EnableProvidersTimeLog
            {
                get
                {
                    var isEnable = GetBooleanValue("EnableProvidersTimeLog");

                    return isEnable;
                }
            }

            ///<summary>
            /// Exception durumlarında yapılacak Log işlemlerinin hangi katman üzerinden yapılacağını belirler
            ///</summary>
            public sealed class Exceptions
            {
                ///<summary>
                /// Log2Db4WebExceptions
                ///</summary>
                public static readonly string Log2Db4WebExceptions = ConfigurationManager.AppSettings["Log2Db4WebExceptions"];

                ///<summary>
                /// Log2FlightExceptionCategoryName
                ///</summary>
                public static readonly string Log2FlightExceptionCategoryName = ConfigurationManager.AppSettings["Log2FlightExceptionCategoryName"];
            }

            ///<summary>
            /// Rezervasyon durumlarında yapılacak Log işlemlerinin hangi katman üzerinden yapılacağını belirler
            ///</summary>
            public sealed class Reservations
            {
                ///<summary>
                /// Log2Db4Reservation
                ///</summary>
                public static readonly string Log2Db4Reservation = ConfigurationManager.AppSettings["Log2Db4Reservation"];

            }

            ///<summary>
            /// Satın alma durumlarında yapılacak Log işlemlerinin hangi katman üzerinden yapılacağını belirler
            ///</summary>
            public sealed class Orders
            {
                ///<summary>
                /// Log2Db4Orders
                ///</summary>
                public static readonly string Log2Db4Orders = ConfigurationManager.AppSettings["Log2Db4Orders"];
            }

            ///<summary>
            /// Satın alma durumlarında yapılacak Log işlemlerinin hangi katman üzerinden yapılacağını belirler
            ///</summary>
            public sealed class StaticDataServices
            {
                ///<summary>
                /// Log2Db4Orders
                ///</summary>
                public static readonly string Log2Db4StaticDataServices = ConfigurationManager.AppSettings["Log2Db4StaticDataServices"];
            }

            ///<summary>
            /// Ödeme durumlarında yapılacak Log işlemlerinin hangi katman üzerinden yapılacağını belirler
            ///</summary>
            public sealed class Payment
            {
                ///<summary>
                /// Log2Db4OrderPaymentUserTransaction
                /// POS tarafında para cekme ve iade/iptal durumlarında
                ///</summary>
                public static readonly string Log2Db4OrderPaymentUserTransaction = ConfigurationManager.AppSettings["Log2Db4OrderPaymentUserTransaction"];

                ///<summary>
                /// Log2Db4OrderPaymentLogTransaction
                /// POS tarafında para cekme ve iade/iptal durumlarında begin/end transaction logları için
                ///</summary>
                public static readonly string Log2Db4OrderPaymentLogTransaction = ConfigurationManager.AppSettings["Log2Db4OrderPaymentLogTransaction"];

                ///<summary>
                /// EnableLog2Db4OrderPaymentLogTransaction
                ///</summary>
                public static bool EnableLog2Db4OrderPaymentLogTransaction
                {
                    get
                    {
                        var isEnable = GetBooleanValue("EnableLog2Db4OrderPaymentLogTransaction");

                        return isEnable;
                    }
                }
            }

            ///<summary>
            /// Promosyon işlem durumlarında yapılacak Log işlemlerinin hangi katman üzerinden yapılacağını belirler
            ///</summary>
            public sealed class Promotions
            {
                ///<summary>
                /// Log2Db4OrderPaymentUserTransaction
                /// POS tarafında para cekme ve iade/iptal durumlarında
                ///</summary>
                public static readonly string Log2Db4PromotionTransaction = ConfigurationManager.AppSettings["Log2Db4PromotionTransaction"];

                ///<summary>
                /// EnableLog2Db4PromotionTransaction
                ///</summary>
                public static bool EnableLog2Db4PromotionTransaction
                {
                    get
                    {
                        var isEnable = GetBooleanValue("EnableLog2Db4PromotionTransaction");

                        return isEnable;
                    }
                }
            }

            ///<summary>
            /// Yönetim ekranlarındaki log işlemleri için kullanılır.
            ///</summary>
            public sealed class Management
            {
                ///<summary>
                ///</summary>
                public static readonly string LogTitleForCurrency = "[YKM]=>[Management]=>[Currency]";

                ///<summary>
                ///</summary>
                public static readonly string LogTitleForServiceFee = "[YKM]=>[Management]=>[ServiceFee]";
            }
        }

        ///<summary>
        /// Validasyon işlemlerinin static property değerlerine erişimi sağlayan sınıf
        ///</summary>
        public sealed class Validations
        {
            ///<summary>
            /// Servislerdeki validasyon sırasında loglama yapılıp yapılamayacağını belirtir
            ///</summary>
            public static bool EnableValidations
            {
                get
                {
                    var isEnable = GetBooleanValue("EnableValidations");

                    return isEnable;
                }
            }

        }

        ///<summary>
        /// Cache işlemlerinde kullanılacak key değerlerinin bulunduğu yer
        ///</summary>
        public sealed class Caching
        {
            /// <summary>
            /// UI Cache işlemlerinde kullanılacak key değerlerinin bulunduğu yer
            /// </summary>
            public sealed class UI
            {
                ///<summary>
                /// FlightHelper.GetAirportByCode methodu içerisinde kullanılan cache key değeri
                ///</summary>
                public static readonly string JxControllerGetCities = "LOGI.Framework.TravelPortal.UI.Controllers.JxControllerGetCities";
            }

            ///<summary>
            /// FlightHelper.GetAirportByCode methodu içerisinde kullanılan cache key değeri
            ///</summary>
            public static readonly string FlightHelperGetAirportByCode = "LOGI.Framework.TravelPortal.Core.Business.Flights.FlightHelperGetAirportByCode";

            ///<summary>
            /// FlightHelper.GetAirlineByCode methodu içerisinde kullanılan cache key değeri
            ///</summary>
            public static readonly string FlightHelperGetAirlineByCode = "LOGI.Framework.TravelPortal.Core.Business.Flights.FlightHelperGetAirlineByCode";

            ///<summary>
            /// FlightHelper.GetAirplaneByCode methodu içerisinde kullanılan cache key değeri
            ///</summary>
            public static readonly string FlightHelperGetAirplaneByCode = "LOGI.Framework.TravelPortal.Core.Business.Flights.FlightHelperGetAirplaneByCode";

            ///<summary>
            /// GetFlightServicesCabinClasses methodu içerisinde kullanılan cache key değeri
            ///</summary>
            public static readonly string FlightGetFlightServicesCabinClasses = "LOGI.Framework.TravelPortal.Core.Business.Flights.FlightGetFlightServicesCabinClasses";
        }

        ///<summary>
        /// Log işlemlerinin static property değerlerine erişimi sağlayan sınıf
        ///</summary>
        public sealed class Security
        {
            ///<summary>
            /// Changing this value will invalidate outstanding authentication cookies
            ///</summary>
            public static readonly string AuthVersion = ConfigurationManager.AppSettings["AuthVersion"];

            ///<summary>
            /// Number of minutes that the auth cookie persists in the browser.
            ///</summary>
            public static readonly string AuthCookiePersistMinutes = ConfigurationManager.AppSettings["AuthCookiePersistMinutes"];

            ///<summary>
            /// Number of minutes that the anonymous cookie persists in the browser.
            ///</summary>
            public static readonly string AnonCookiePersistMinutes = ConfigurationManager.AppSettings["AnonCookiePersistMinutes"];

            ///<summary>
            /// 
            ///</summary>
            public static readonly string KeySalt = ConfigurationManager.AppSettings["KeySalt"];

            ///<summary>
            /// Password complexity validation rule
            ///</summary>
            public static readonly string PasswordMinRequiredNonAlphanumericCharacters = ConfigurationManager.AppSettings["PasswordMinRequiredNonAlphanumericCharacters"];

            ///<summary>
            /// Password length validation rule
            ///</summary>
            public static readonly string PasswordMinRequiredLength = ConfigurationManager.AppSettings["PasswordMinRequiredLength"];

            ///<summary>
            /// FacebookConnect App Key
            ///</summary>
            public static readonly string FacebookConnectAppKey = ConfigurationManager.AppSettings["FacebookConnectAppKey"];


            ///<summary>
            /// Should anonymous users to database? Useful for when critical data is stored about anonymous users
            ///</summary>
            public static bool PersistAnonymousSessionsInRepository
            {
                get
                {
                    var isEnable = GetBooleanValue("PersistAnonymousSessionsInRepository");

                    return isEnable;
                }
            }

            ///<summary>
            /// Enable or disbale SSL for SMTP communication
            ///</summary>
            public static bool SmtpEnableSsl
            {
                get
                {
                    var isEnable = GetBooleanValue("SmtpEnableSsl");

                    return isEnable;
                }
            }

            ///<summary>
            /// Enable or disable smtp email verification
            ///</summary>
            public static bool VerifyEmailAddresses
            {
                get
                {
                    var isEnable = GetBooleanValue("VerifyEmailAddresses");

                    return isEnable;
                }
            }

            /// <summary>
            /// TravelPortalContext üzerindeki PortalSession objesi için süre
            /// </summary>
            public static string PortalSessionTimeOutAsSecond
            {
                get
                {
                    return ConfigurationManager.AppSettings["PortalSessionTimeOutLimitAsSecond"];
                }
            }
        }

        public sealed class TravelPortalCore
        {
            ///<summary>
            /// Core uygulamada loglanacak mesajın prefixini belirtir ([{0} -XXXX] formatında 0.ncı parametre)
            ///</summary>
            public static readonly string LogPrefix = "Core";

            ///<summary>
            /// TravelPortalContext
            ///</summary>
            public static readonly string TravelPortalContextKey = "__TravelPortalContext__";

            ///<summary>
            /// Core uygulamada anonim user adı
            ///</summary>
            public static readonly string AnonymousUserName = "__ANONYMOUS__";

            public sealed class Payment
            {
                /// <summary>
                /// Satın alma  isleminde taksitler hem karta hem de kart + urune gore gelirse hangi secimin taksitleri gozukucek,buna burdaki key ile portal kurallarında tanımlı degere bakılır
                /// </summary>
                public static readonly string UseProductInstallmentsPortalKey = ConfigurationManager.AppSettings["UseProductInstallmentsPortalKey"];

                /// <summary>
                /// InstallmentContainers
                /// </summary>
                public static readonly string InstallmentContainerToken = "InstallmentContainers";

                /// <summary>
                /// BankContainers
                /// </summary>
                public static readonly string BankContainerToken = "BankContainers";

                /// <summary>
                /// BankCardContainers
                /// </summary>
                public static readonly string BankCardContainerToken = "BankCardContainers";

                /// <summary>
                /// BankCardInstallmentsContainers
                /// </summary>
                public static readonly string BankCardInstallmentsContainerToken = "BankCardInstallmentsContainers";

                /// <summary>
                /// BankCardInstallmentsContainers
                /// </summary>
                public static readonly string BankCardInstallmentsByProductContainerToken = "BankCardInstallmentsByProductContainers";

                /// <summary>
                /// Servisin kullanıma açılacağı pos portal adı
                /// </summary>
                public static readonly string POSPortalName = ConfigurationManager.AppSettings["POSPortalName"];
            }

            ///<summary>
            /// Core UI uygulamada loglanacak mesajın prefixini belirtir ([{0} -XXXX] formatında 0.ncı parametre)
            ///</summary>
            public sealed class UI
            {
                /// <summary>
                /// "WEB-UI SiteRootKey"
                /// </summary>
                public static readonly string SiteRootKey = "WEB-UI-SiteRootKey";

                /// <summary>
                /// "WEB-UI"
                /// </summary>
                public static readonly string LogMessagePrefix = "WEB-UI";

                /// <summary>
                /// "[{0}-TimeLog]", LogMessagePrefix
                /// </summary>
                public static readonly string TimeLogPrefix = string.Format("[{0}-TimeLog]", LogMessagePrefix);

                ///<summary>
                /// Core UI uygulamada session'a atılacak custom session key'i
                ///</summary>
                public static readonly string CurrentSessionKey = "_CurrentSession_";

                ///<summary>
                /// Core UI uygulamada session'a atılacak travelportalsession'a ait sessionID key'i
                ///</summary>
                public static readonly string CurrentSessionIdKey = "TravelPortalSessionId";

                ///<summary>
                /// SiteHost
                ///</summary>
                public static readonly string CDNNames = "CDNHelper.CDNNames";

                public sealed class PassengerInfo
                {
                    public static readonly string RootPrefix = "__";
                    public static readonly string SpecialRequestInfoToken = "SRI";
                    public static readonly string MilesCardInfoToken = "MCI";
                    public static readonly string PersonInfoToken = "Person";
                    public static readonly string ContactInfoToken = "ContactInfo";
                    public static readonly string PassengerSaveToken = "SaveThisPassenger";
                    public static readonly string MasterContact = "MasterContact";
                    public static readonly string PassengerId = "PassengerId";
                    public static readonly string PassengerDbId = "PassengerDbId";
                    public static readonly string AccountName = "PassengerAccountName";
                }
            }

            /// <summary>
            /// Loglama yapılan tablolarda extra kolonların EntLib Listener larda tanımlansında kullanılan keyler.
            /// </summary>
            public sealed class LogColumns
            {
                public sealed class ModulesLogData
                {
                    ///<summary>
                    /// SearchId
                    ///</summary>
                    public static readonly string ReferenceNumber = "ReferenceNumber";

                    ///<summary>
                    /// SourceType
                    ///</summary>
                    public static readonly string SourceType = "SourceType";

                    ///<summary>
                    /// ModuleId
                    ///</summary>
                    public static readonly string ModuleId = "ModuleId";

                    ///<summary>
                    /// ModuleItemId
                    ///</summary>
                    public static readonly string ModuleItemId = "ModuleItemId";

                    ///<summary>
                    /// ProcessType
                    ///</summary>
                    public static readonly string ProcessType = "ProcessType";

                    ///<summary>
                    /// ProcessType
                    ///</summary>
                    public static readonly string ProcessTitle = "ProcessTitle";

                    ///<summary>
                    /// QuarableXmlData
                    ///</summary>
                    public static readonly string QuarableXmlData = "QuarableXmlData";

                    ///<summary>
                    /// Data
                    ///</summary>
                    public static readonly string OriginalData = "OriginalData";
                }

                public sealed class WebAppLogData
                {
                    ///<summary>
                    /// SearchId
                    ///</summary>
                    public static readonly string SearchId = "SearchId";

                    ///<summary>
                    /// AffiliateCode
                    ///</summary>
                    public static readonly string AffiliateCode = "AffiliateCode";

                    ///<summary>
                    /// SessionId
                    ///</summary>
                    public static readonly string UserName = "UserName";

                    ///<summary>
                    /// UserName
                    ///</summary>
                    public static readonly string SessionId = "SessionId";
                }

                public sealed class OrdersLogData
                {
                    ///<summary>
                    /// AffiliateCode
                    ///</summary>
                    public static readonly string AffiliateCode = "AffiliateCode";

                    ///<summary>
                    /// SessionId
                    ///</summary>
                    public static readonly string UserName = "UserName";

                    ///<summary>
                    /// UserName
                    ///</summary>
                    public static readonly string SessionId = "SessionId";

                    ///<summary>
                    /// BasketWebRefID
                    ///</summary>
                    public static readonly string BasketWebRefID = "BasketWebRefID";

                    ///<summary>
                    /// ItemType
                    ///</summary>
                    public static readonly string ItemType = "ItemType";
                }

                public sealed class ResevationsLogData
                {
                    ///<summary>
                    /// AffiliateCode
                    ///</summary>
                    public static readonly string AffiliateCode = "AffiliateCode";

                    ///<summary>
                    /// SessionId
                    ///</summary>
                    public static readonly string UserName = "UserName";

                    ///<summary>
                    /// UserName
                    ///</summary>
                    public static readonly string SessionId = "SessionId";

                    ///<summary>
                    /// BasketWebRefID
                    ///</summary>
                    public static readonly string BasketWebRefID = "BasketWebRefID";

                    ///<summary>
                    /// ItemType
                    ///</summary>
                    public static readonly string ItemType = "ItemType";
                }

                public sealed class OrderPaymentTransactionsLogData
                {
                    ///<summary>
                    /// AffiliateCode
                    ///</summary>
                    public static readonly string AffiliateCode = "AffiliateCode";

                    ///<summary>
                    /// SessionId
                    ///</summary>
                    public static readonly string UserName = "UserName";

                    ///<summary>
                    /// UserName
                    ///</summary>
                    public static readonly string SessionId = "SessionId";

                    ///<summary>
                    /// BasketWebRefID
                    ///</summary>
                    public static readonly string BasketWebRefID = "BasketWebRefID";

                    ///<summary>
                    /// VPOSSalesTransactionId
                    ///</summary>
                    public static readonly string VPOSSalesTransactionId = "VPOSSalesTransactionId";

                }
            }

            /// <summary>
            /// Template'lere gönderilen key'ler buraya
            /// </summary>
            public sealed class TemplateKeys
            {
                public sealed class Flight
                {
                    ///<summary>
                    ///Uçuş için template'lere gönderilen key'ler buraya
                    ///</summary>
                    public static readonly string ETicketInfoDataKey = "EticketData";

                    ///<summary>
                    ///Eticket gösteriminde 
                    ///</summary>
                    public static readonly string ETicketInfoOverrideRoot = "OverrideRoot";

                    public sealed class Amadeus
                    {
                        public sealed class SpecialRequests
                        {
                            ///<summary>
                            ///SeatChoice
                            ///</summary>
                            public static readonly string SeatChoice = "SeatChoice";

                            ///<summary>
                            ///MealChoice
                            ///</summary>
                            public static readonly string MealChoice = "Meal";

                            ///<summary>
                            ///AnimalChoice
                            ///</summary>
                            public static readonly string AnimalChoice = "Pet";

                            ///<summary>
                            ///BicycleChoice
                            ///</summary>
                            public static readonly string BicycleChoice = "Bicycle";

                            ///<summary>
                            ///MeetAndAssistChoice
                            ///</summary>
                            public static readonly string MeetAndAssistChoice = "Assist";

                            ///<summary>
                            ///WhellChair
                            ///</summary>
                            public static readonly string WhellChair = "WhellChair";

                            ///<summary>
                            ///SnowboardChoice
                            ///</summary>
                            public static readonly string SnowboardChoice = "Snowboard";

                            ///<summary>
                            ///SnowboardWeightChoiceChoice
                            ///</summary>
                            public static readonly string SnowboardWeightChoice = "Snowboard_Weight";

                            ///<summary>
                            ///Snowboard_LengthChoiceChoice
                            ///</summary>
                            public static readonly string Snowboard_LengthChoice = "Snowboard_Length";

                            ///<summary>
                            ///SurfboardChoice
                            ///</summary>
                            public static readonly string SurfboardChoice = "Surfboard";

                            ///<summary>
                            ///Surfboard_WeightChoice
                            ///</summary>
                            public static readonly string Surfboard_WeightChoice = "Surfboard_Weight";

                            ///<summary>
                            ///Surfboard_Widthhoice
                            ///</summary>
                            public static readonly string Surfboard_WidthChoice = "Surfboard_Width";

                            ///<summary>
                            ///Surfboard_Heighthoice
                            ///</summary>
                            public static readonly string Surfboard_HeightChoice = "Surfboard_Height";

                            #region Visa
                            ///<summary>
                            ///VisaNumber
                            ///</summary>
                            public static readonly string VisaNumber = "VisaNumber";

                            ///<summary>
                            ///PlaceOfBirth
                            ///</summary>
                            public static readonly string VisaPlaceOfBirth = "PlaceOfBirth";

                            ///<summary>
                            ///DateOfIssue
                            ///</summary>
                            public static readonly string VisaDateOfIssue = "DateOfIssue";

                            ///<summary>
                            ///PlaceOfIssue
                            ///</summary>
                            public static readonly string VisaPlaceOfIssue = "PlaceOfIssue";

                            ///<summary>
                            ///VisaCountry
                            ///</summary>
                            public static readonly string VisaCountry = "VisaCountry";
                            #endregion

                            #region Passport
                            ///<summary>
                            ///PassportNumber
                            ///</summary>
                            public static readonly string PassportNumber = "PassportNumber";

                            ///<summary>
                            ///PassportEndDate
                            ///</summary>
                            public static readonly string PassportEndDate = "PassportEndDate";

                            ///<summary>
                            ///PassportCountry
                            ///</summary>
                            public static readonly string PassportCountry = "PassportCountry";
                            #endregion

                        }

                        public sealed class MilesCard
                        {
                            ///<summary>
                            ///FFCardCode
                            ///</summary>
                            public static readonly string FFCardCode = "Amadeus";

                            ///<summary>
                            ///FFCardNo
                            ///</summary>
                            public static readonly string FFCardNo = "AmadeusNo";


                        }
                    }

                }
                public sealed class EventGuide
                {
                    public sealed class OnlineTicket
                    {
                        public sealed class SpecialRequests
                        {
                            static SpecialRequests()
                            {
                                ShippingInfo_AddressType = "AddressType";
                            }


                            ///<summary>
                            /// Kargo için adres tipi hotel veya residency
                            ///</summary>
                            public static readonly string ShippingInfo_AddressType = "AddressType";

                            public static readonly string ShippingInfo_Address = "Address";

                            public static readonly string ShippingInfo_PostCode = "PostCode";

                            public static readonly string ShippingInfo_City = "City";

                            public static readonly string ShippingInfo_State = "State";

                            public static readonly string ShippingInfo_Country = "Country";

                            public static readonly string ShippingInfo_Company = "Company";

                            public static readonly string ShippingInfo_HotelName = "HotelName";

                            public static readonly string ShippingInfo_HotelPhone = "HotelPhone";

                            public static readonly string ShippingInfo_HotelArrivalDate = "ArrivalDate";

                            public static readonly string ShippingInfo_HotelArrivalTime = "ArrivalTime";

                            public static readonly string ShippingInfo_BookingInfo = "BookingInfo";

                        }
                    }
                }
            }

            /// <summary>
            /// Membership değerleri
            /// </summary>
            public sealed class Membership
            {
                /// <summary>
                /// Servisin kullanıma açılacağı Membership portal adı
                /// </summary>
                public static readonly string MembershipPortalName = ConfigurationManager.AppSettings["MembershipPortalName"];

                /// <summary>
                /// Kaydedilecek Kullanıcıların otomatik atanacak rol code
                /// </summary>
                public static readonly string RoleCode = ConfigurationManager.AppSettings["MembershipRoleCode"];

                /// <summary>
                /// Kaydedilecek Kullanıcıların otomatik atanacak profil code
                /// </summary>
                public static readonly string ProfileCode = ConfigurationManager.AppSettings["MembershipProfileCode"];
            }

            /// <summary>
            /// Promotion değerleri
            /// </summary>
            public sealed class Promotion
            {
                /// <summary>
                /// Servisin kullanıma açılacağı Promotion portal adı
                /// </summary>
                public static readonly string PromotionPortalName = ConfigurationManager.AppSettings["PromotionPortalName"];

                /// <summary>
                /// Promosyon template'lerinin root klasörü.
                /// </summary>
                public static string PromotionFilterTemplateFilePath
                {
                    get
                    {
                        var templateRoot = ConfigurationManager.AppSettings["PromotionFilterTemplateFileDirectory"];
                        var returnValue = HttpContext.Current.Server.MapPath(templateRoot);

                        return returnValue;
                    }

                }

                /// <summary>
                /// ModuleMaps
                /// </summary>
                public static string ModuleMapFileName
                {
                    get
                    {
                        var returnValue = "ModuleMaps";

                        return returnValue;
                    }

                }

                /// <summary>
                /// Promosyon template'lerinin file name formatını belirtir.
                /// File Format:CampaignFilterTemplate.{Module Name}.ctf
                /// </summary>
                public static string PromotionFilterTemplateFileFormat
                {
                    get
                    {
                        var returnValue = ConfigurationManager.AppSettings["PromotionFilterTemplateFileFormat"];

                        return returnValue;
                    }

                }

            }
        }

        /// <summary>
        /// AppConfig'den ilgili sub key yoksa site üzerinden alır
        /// </summary>
        /// <param name="siteAppSettingValue"></param>
        /// <param name="subAppSettingKey"></param>
        /// <returns></returns>
        private static string GetAppConfigValueFromSiteIfEmpty(string siteAppSettingValue, string subAppSettingKey)
        {
            var appSettingValue = ConfigurationManager.AppSettings[subAppSettingKey];

            var retValue =
                    !string.IsNullOrEmpty(appSettingValue)
                        ? appSettingValue
                        : siteAppSettingValue;

            return retValue;
        }

        ///<summary>
        /// Satın alma yada rezerve etme dışındaki requestlerde time out süresi
        ///</summary>
        ///<param name="providerName"></param>
        ///<returns></returns>
        public static string GetSessionTimeOutLimitAsSecondByProvider(string providerName)
        {
            var retValue = GetAppConfigValueFromSiteIfEmpty(Security.PortalSessionTimeOutAsSecond, providerName + "SessionTimeOutLimitAsSecond");

            return retValue;
        }

        ///<summary>
        /// Satın alma yada rezerve etme dışındaki requestlerde time out süresi
        ///</summary>
        ///<param name="providerName"></param>
        ///<returns></returns>
        public static string GetRequestTimeLimitAsSecondByProvider(string providerName)
        {
            var retValue = GetAppConfigValueFromSiteIfEmpty(Web.RequestTimeLimitAsSecond, providerName + "RequestTimeLimitAsSecond");

            return retValue;
        }

        ///<summary>
        /// Satın alma yada rezerve etme time out süresi
        ///</summary>
        ///<param name="providerName"></param>
        ///<returns></returns>
        public static string GetBookRequestTimeLimitAsSecondByProvider(string providerName)
        {
            var retValue = GetAppConfigValueFromSiteIfEmpty(Web.BookRequestTimeLimitAsSecond, providerName + "BookRequestTimeLimitAsSecond");

            return retValue;
        }

        ///<summary>
        /// Bir uçuş servisin promosyon sınıflarını belirler
        ///</summary>
        ///<param name="type"></param>
        ///<returns></returns>
        public static string GetPromotionClassesBy(string type)
        {
            var retValue = ConfigurationManager.AppSettings[type + "PromotionClasses"];

            return retValue;
        }

        ///<summary>
        ///</summary>
        ///<param name="type"></param>
        ///<returns></returns>
        public static string GetBasketItemTemplateFile(IDictionary tokens, string moduleName)
        {
            /*
            <add key="ModuleBasketItemTemplateFile" value="Templates/Basket/BasketItem.{ModuleName}Item.cshtml"/>
             
            */

            var retValue = ConfigurationManager.AppSettings["ModuleBasketItemTemplateFile"];

            if (!string.IsNullOrEmpty(retValue))
            {
                retValue = retValue.Replace("{ModuleName}", moduleName);
            }

            retValue = ConstantHelper.Web.getBasketTemplateFileFullPath(tokens, retValue);

            return retValue;
        }

        #region BasketItemReservationDetail
        ///<summary>
        ///</summary>
        ///<param name="type"></param>
        ///<returns></returns>
        public static string GetBasketItemReservationDetailTemplateFile(string type)
        {
            var retValue = ConfigurationManager.AppSettings[type + "ReservationDetailTemplateFile"];

            return retValue;
        }

        ///<summary>
        ///</summary>
        ///<param name="type"></param>
        ///<returns></returns>
        public static string GetBasketItemReservationDetailItemTemplateFile(string type)
        {
            var retValue = ConfigurationManager.AppSettings[type + "ReservationDetailItemTemplateFile"];

            return retValue;
        }

        ///<summary>
        ///</summary>
        ///<param name="type"></param>
        ///<returns></returns>
        public static string GetBasketItemReservationDetailItemPriceTemplateFile(string type)
        {
            var retValue = ConfigurationManager.AppSettings[type + "ReservationDetailItemPriceTemplateFile"];

            return retValue;
        }
        #endregion


        ///<summary>
        ///</summary>
        ///<param name="type"></param>
        ///<returns></returns>
        public static string GetCurrencyConverterBy(string type)
        {
            var retValue = ConfigurationManager.AppSettings[type + "CurrencyConverter"];

            return retValue;
        }

        ///<summary>
        ///</summary>
        ///<param name="type"></param>
        ///<returns></returns>
        public static bool GetStateOfTheService(string type)
        {
            var retValue = GetBooleanValue(type + "StateOfTheService");

            return retValue;
        }

        ///<summary>
        ///</summary>
        ///<param name="type"></param>
        ///<returns></returns>
        public static bool GetFlightEnableServiceFee(string type)
        {
            var root = FlightProviders.EnableServiceFee;
            var retValue = GetBooleanValue(type + "EnableServiceFee");

            return root && retValue;
        }

        ///<summary>
        /// Servisin test yada prod durumunu belirtir.
        ///</summary>
        ///<param name="type"></param>
        ///<returns></returns>
        public static bool GetModeOfTheService(string type)
        {
            var retValue = GetBooleanValue(type + "InTestMode");

            return retValue;
        }

        #region Log2MailToList
        ///<summary>
        /// Log2MailToList
        ///</summary>
        ///<param name="type"></param>
        ///<returns></returns>
        private static string GetLog2MailToListByProvider(string parentValue, string type)
        {
            var typeValue = ConfigurationManager.AppSettings[type + "Log2MailToList"];

            var returnValue = string.Format("{0};{1};", parentValue, typeValue);

            return returnValue.Replace(";;", ";");
        }

        ///<summary>
        /// Log2MailToList
        ///</summary>
        ///<param name="type"></param>
        ///<returns></returns>
        public static string GetLog2MailToListByProvider(string type)
        {
            var typeValue = ConfigurationManager.AppSettings[type + "Log2MailToList"];

            var returnValue = string.Format("{0};{1};", typeValue);

            return returnValue.Replace(";;", ";");
        }
        #endregion

        ///<summary>
        ///</summary>
        ///<param name="type"></param>
        ///<returns></returns>
        public static string GetETicketTemplateFilePathByModuleItem(IDictionary tokens, string moduleName, string moduleItemName)
        {
            /*
             * Templates/ETicket/ETicket.{ModuleName}.{ModuleItemName}.cshtml
            */
            var retValue = ConfigurationManager.AppSettings["ModuleItemETicketTemplateFilePath"];

            if (tokens.Contains(TravelPortalCore.TemplateKeys.Flight.ETicketInfoOverrideRoot))
            {
                var newRoot = tokens[TravelPortalCore.TemplateKeys.Flight.ETicketInfoOverrideRoot].ToString();

                if (!string.IsNullOrEmpty(newRoot))
                {
                    retValue = retValue.Replace("Templates/ETicket", newRoot);
                }
            }

            if (!string.IsNullOrEmpty(retValue))
            {
                retValue = retValue.Replace("{ModuleName}", moduleName);
                retValue = retValue.Replace("{ModuleItemName}", moduleItemName);
            }
            return retValue;
        }

        private static bool GetBooleanValue(string appSettingsKey, bool defaultValue = false)
        {
            var appValue = ConfigurationManager.AppSettings[appSettingsKey];

            var isTrue = defaultValue;

            //EnableFlightCoreValidations config değeri bool türünde ise çevir gönder 
            if (Boolean.TryParse(appValue, out isTrue))
            {
                return isTrue;
            }

            return isTrue;
        }

    }
}
