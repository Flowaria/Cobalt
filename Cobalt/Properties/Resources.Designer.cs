﻿//------------------------------------------------------------------------------
// <auto-generated>
//     이 코드는 도구를 사용하여 생성되었습니다.
//     런타임 버전:4.0.30319.42000
//
//     파일 내용을 변경하면 잘못된 동작이 발생할 수 있으며, 코드를 다시 생성하면
//     이러한 변경 내용이 손실됩니다.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Cobalt.Properties {
    using System;
    
    
    /// <summary>
    ///   지역화된 문자열 등을 찾기 위한 강력한 형식의 리소스 클래스입니다.
    /// </summary>
    // 이 클래스는 ResGen 또는 Visual Studio와 같은 도구를 통해 StronglyTypedResourceBuilder
    // 클래스에서 자동으로 생성되었습니다.
    // 멤버를 추가하거나 제거하려면 .ResX 파일을 편집한 다음 /str 옵션을 사용하여 ResGen을
    // 다시 실행하거나 VS 프로젝트를 다시 빌드하십시오.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   이 클래스에서 사용하는 캐시된 ResourceManager 인스턴스를 반환합니다.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Cobalt.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   이 강력한 형식의 리소스 클래스를 사용하여 모든 리소스 조회에 대한 현재 스레드의 CurrentUICulture
        ///   속성을 재정의합니다.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   &lt;Config&gt;
        ///  &lt;API&gt;
        ///    &lt;!-- 반드시! http://steamcommunity.com/dev/apikey 에서 API 키를 받아 작성해주세요 --&gt;
        ///    &lt;Key&gt;EDITPLEASE&lt;/Key&gt;
        ///    &lt;BaseLang&gt;ko_KR&lt;/BaseLang&gt;
        ///  &lt;/API&gt;
        ///  &lt;MESSAGE&gt;
        ///    &lt;ItemSchemaLoad&gt;아이템 스캐마 불러오는 중 . . .&lt;/ItemSchemaLoad&gt;
        ///    &lt;ItemImageDownload&gt;이미지 다운로드 중 . . .&lt;/ItemImageDownload&gt;
        ///    &lt;TemplateLoad&gt;템플릿 불러오는 중 . . .&lt;/TemplateLoad&gt;
        ///  &lt;/MESSAGE&gt;
        ///&lt;/Config&gt;과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        internal static string config {
            get {
                return ResourceManager.GetString("config", resourceCulture);
            }
        }
        
        /// <summary>
        ///   (아이콘)과(와) 유사한 System.Drawing.Icon 형식의 지역화된 리소스를 찾습니다.
        /// </summary>
        internal static System.Drawing.Icon icon {
            get {
                object obj = ResourceManager.GetObject("icon", resourceCulture);
                return ((System.Drawing.Icon)(obj));
            }
        }
        
        /// <summary>
        ///   System.Drawing.Bitmap 형식의 지역화된 리소스를 찾습니다.
        /// </summary>
        internal static System.Drawing.Bitmap Mask01 {
            get {
                object obj = ResourceManager.GetObject("Mask01", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   WaveSchedule
        ///{
        ///	Templates
        ///	{
        ///
        ///		T_TFGateBot_Scout_Melee
        ///		{
        ///			Class Scout
        ///
        ///			EventChangeAttributes
        ///			{
        ///				Default
        ///				{
        ///					Tag bot_gatebot 				// having these will cause bots to run towards gates
        ///					Tag nav_prefer_gate1_flank		// having these will cause bots to run towards gates
        ///					BehaviorModifiers push			// having these will cause bots to run towards gates
        ///					Attributes IgnoreFlag 			// having these will cause bots to run towards gates
        ///
        ///					Item &quot;MvM GateBot Light Scout&quot;
        ///		[나머지 문자열은 잘림]&quot;;과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        internal static string robot_gatebot {
            get {
                return ResourceManager.GetString("robot_gatebot", resourceCulture);
            }
        }
        
        /// <summary>
        ///   // -- These attributes must attached to a weapon using ItemAttributes and then ItemName --
        ///
        ///// &quot;Blast radius decreased&quot;
        ///// &quot;Reload time decreased&quot;
        ///// &quot;airblast pushback scale&quot;
        ///// &quot;arrow mastery&quot;
        ///// &quot;attack projectiles&quot;
        ///// &quot;bullets per shot bonus&quot;
        ///// &quot;clip size bonus&quot;
        ///// &quot;clip size penalty&quot;
        ///// &quot;clip size upgrade atomic&quot;
        ///// &quot;cloak consume rate increased&quot;
        ///// &quot;critboost on kill&quot;
        ///// &quot;damage bonus&quot;
        ///// &quot;damage causes airblast&quot;
        ///// &quot;damage penalty&quot;
        ///// &quot;effect bar recharge rate increased&quot;
        ///// &quot;faster [나머지 문자열은 잘림]&quot;;과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        internal static string robot_giant {
            get {
                return ResourceManager.GetString("robot_giant", resourceCulture);
            }
        }
        
        /// <summary>
        ///   // -- These attributes must attached to a weapon using ItemAttributes and then ItemName --
        ///
        ///// &quot;Blast radius decreased&quot;
        ///// &quot;Reload time decreased&quot;
        ///// &quot;airblast pushback scale&quot;
        ///// &quot;arrow mastery&quot;
        ///// &quot;attack projectiles&quot;
        ///// &quot;bullets per shot bonus&quot;
        ///// &quot;clip size bonus&quot;
        ///// &quot;clip size penalty&quot;
        ///// &quot;clip size upgrade atomic&quot;
        ///// &quot;cloak consume rate increased&quot;
        ///// &quot;critboost on kill&quot;
        ///// &quot;damage bonus&quot;
        ///// &quot;damage causes airblast&quot;
        ///// &quot;damage penalty&quot;
        ///// &quot;effect bar recharge rate increased&quot;
        ///// &quot;faster [나머지 문자열은 잘림]&quot;;과(와) 유사한 지역화된 문자열을 찾습니다.
        /// </summary>
        internal static string robot_standard {
            get {
                return ResourceManager.GetString("robot_standard", resourceCulture);
            }
        }
        
        /// <summary>
        ///   System.Drawing.Bitmap 형식의 지역화된 리소스를 찾습니다.
        /// </summary>
        internal static System.Drawing.Bitmap Splash0 {
            get {
                object obj = ResourceManager.GetObject("Splash0", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   System.Drawing.Bitmap 형식의 지역화된 리소스를 찾습니다.
        /// </summary>
        internal static System.Drawing.Bitmap Splash1 {
            get {
                object obj = ResourceManager.GetObject("Splash1", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   System.Drawing.Bitmap 형식의 지역화된 리소스를 찾습니다.
        /// </summary>
        internal static System.Drawing.Bitmap Splash2 {
            get {
                object obj = ResourceManager.GetObject("Splash2", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   System.Drawing.Bitmap 형식의 지역화된 리소스를 찾습니다.
        /// </summary>
        internal static System.Drawing.Bitmap Splash3 {
            get {
                object obj = ResourceManager.GetObject("Splash3", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   System.Drawing.Bitmap 형식의 지역화된 리소스를 찾습니다.
        /// </summary>
        internal static System.Drawing.Bitmap Splash4 {
            get {
                object obj = ResourceManager.GetObject("Splash4", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   System.Drawing.Bitmap 형식의 지역화된 리소스를 찾습니다.
        /// </summary>
        internal static System.Drawing.Bitmap Splash5 {
            get {
                object obj = ResourceManager.GetObject("Splash5", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   System.Drawing.Bitmap 형식의 지역화된 리소스를 찾습니다.
        /// </summary>
        internal static System.Drawing.Bitmap Splash6 {
            get {
                object obj = ResourceManager.GetObject("Splash6", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   System.Drawing.Bitmap 형식의 지역화된 리소스를 찾습니다.
        /// </summary>
        internal static System.Drawing.Bitmap TempIcon {
            get {
                object obj = ResourceManager.GetObject("TempIcon", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
    }
}
