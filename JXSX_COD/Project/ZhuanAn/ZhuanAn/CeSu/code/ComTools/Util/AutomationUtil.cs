using ComTools.Automation;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Automation;
using System.Windows.Automation.Text;
using System.Windows.Forms;

namespace ComTools.Util
{
    public class AutomationUtil
    {
        #region 获取所有的标签
        /// <summary>
        /// 获取指定窗体所有控件信息
        /// </summary>
        /// <param name="windowClassName"></param>
        /// <param name="windowTitle"></param>
        public static List<AutomationElement> GetAllElements(string windowTitle, string windowClassName)
        {
            //查找子控件
            List<AutomationElement> elements = new List<AutomationElement>();
            //找到目标窗口
            var targetWindowCondition = new AndCondition(
                new PropertyCondition(AutomationElement.ClassNameProperty, windowClassName),
                new PropertyCondition(AutomationElement.NameProperty, windowTitle));
            AutomationElement targetWindow = AutomationElement.RootElement.FindFirst(TreeScope.Children, targetWindowCondition);

            if (targetWindow == null)
            {
                return elements;
            }
            EnumerateChildElements(targetWindow, elements);
            return elements;
        }


        /// <summary>
        /// 递归获取子元素
        /// </summary>
        /// <param name="parentElement"></param>
        private static void EnumerateChildElements(AutomationElement parentElement, List<AutomationElement> elements)
        {
            var childElements = parentElement.FindAll(TreeScope.Children, Condition.TrueCondition);
            foreach (AutomationElement child in childElements)
            {
                elements.Add(child);
                // 递归处理子元素
                EnumerateChildElements(child, elements);
            }
        }
        #endregion

        #region 获取自动化标签
        /// <summary>
        /// 获取窗口的根标签
        /// </summary>
        /// <param name="windowTitle"></param>
        /// <param name="windowClassName"></param>
        /// <returns></returns>
        public static AutomationElement GetWindowAutomationElement(string windowTitle, string windowClassName)
        {
            AndCondition windowCondition = new AndCondition(
               new PropertyCondition(AutomationElement.ClassNameProperty, windowClassName),
               new PropertyCondition(AutomationElement.NameProperty, windowTitle));
            AutomationElement rootElement = AutomationElement.RootElement.FindFirst(TreeScope.Children, windowCondition);
            return rootElement;
        }


        /// <summary>
        /// 获取指定控件的自动化元素
        /// </summary>
        /// <param name="windowTitle"></param>
        /// <param name="automationId"></param>
        /// <param name="controlType"></param>
        /// <returns></returns>
        public static AutomationElement GetAutomationElement(string windowTitle, string automationId, ControlType controlType)
        {
            IntPtr windowHandle = WindowEnumerator.GetWindowHandleByName(windowTitle);
            if (windowHandle != IntPtr.Zero)
            {
                WindowEnumerator.GetWindowInfo(windowHandle, out string windowClassName, out windowTitle);
                return GetAutomationElement(windowTitle, windowClassName, automationId, controlType);
            }
            return null;
        }

        /// <summary>
        /// 获取指定控件的自动化元素
        /// </summary>
        /// <param name="windowTitle"></param>
        /// <param name="windowClassName"></param>
        /// <param name="automationId"></param>
        /// <param name="controlType"></param>
        /// <returns></returns>
        public static AutomationElement GetAutomationElement(string windowTitle, string windowClassName, string automationId, ControlType controlType)
        {
            AutomationElement rootElement = GetWindowAutomationElement(windowTitle, windowClassName);
            if (rootElement != null)
            {
                AndCondition elementCondition = new AndCondition(
                    new PropertyCondition(AutomationElement.AutomationIdProperty, automationId),
                    new PropertyCondition(AutomationElement.ControlTypeProperty, controlType));
                AutomationElement targetElement = rootElement.FindFirst(TreeScope.Descendants, elementCondition);
                if (targetElement != null)
                    return targetElement;
            }
            return null;
        }
        #endregion

        #region 获取控件的值
        public static string GetControlValue(AutomationElement element)
        {
            try
            {
                if (element.Current.ControlType == ControlType.ComboBox || element.Current.ControlType == ControlType.DataGrid)
                {//列表类型，获取选中的值
                    return GetElementSelectd(element);
                }
                else
                {
                    return element.Current.Name;
                }
            }
            catch (Exception)
            {
            }
            return "";
        }

        /// <summary>
        /// 获取控件的值
        /// </summary>
        /// <param name="windowTitle"></param>
        /// <param name="automationId"></param>
        /// <param name="controlType"></param>
        /// <returns></returns>
        public static string GetControlValue(string windowTitle, string automationId, ControlType controlType)
        {
            IntPtr windowHandle = WindowEnumerator.GetWindowHandleByName(windowTitle);
            if (windowHandle != IntPtr.Zero)
            {
                WindowEnumerator.GetWindowInfo(windowHandle, out string windowClassName, out windowTitle);
                return GetControlValue(windowTitle, windowClassName, automationId, controlType);
            }
            return null;
        }

        /// <summary>
        /// 获取控件的值
        /// </summary>
        /// <param name="windowTitle"></param>
        /// <param name="windowClassName"></param>
        /// <param name="automationId"></param>
        /// <param name="controlType"></param>
        /// <returns></returns>
        public static string GetControlValue(string windowTitle, string windowClassName, string automationId, ControlType controlType)
        {
            AndCondition windowCondition = new AndCondition(
                new PropertyCondition(AutomationElement.ClassNameProperty, windowClassName),
                new PropertyCondition(AutomationElement.NameProperty, windowTitle));
            AutomationElement rootElement = AutomationElement.RootElement.FindFirst(TreeScope.Children, windowCondition);
            if (rootElement != null)
            {

                AndCondition elementCondition = new AndCondition(
                    new PropertyCondition(AutomationElement.AutomationIdProperty, automationId),
                    new PropertyCondition(AutomationElement.ControlTypeProperty, controlType));
                AutomationElement targetElement = rootElement.FindFirst(TreeScope.Descendants, elementCondition);
                if (targetElement != null)
                    return GetControlValue(targetElement);
            }
            return null;
        }


        #region  根据不同的控件类型，获取控件的当前的文本

        //文本值获取
        private static string GetElementText(AutomationElement element)
        {
            try
            {
                // 尝试获取TextPattern
                TextPattern textPattern = (TextPattern)element.GetCurrentPattern(TextPattern.Pattern);
                if (textPattern != null)
                {
                    // 获取文档范围
                    TextPatternRange documentRange = textPattern.DocumentRange;

                    // 获取整个文本内容
                    string textContent = documentRange.GetText(-1); // -1表示获取全部文本
                    return textContent;
                }
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("无法获取TextPattern: " + ex.Message);
            }
            return "";
        }


        //值类型控件
        private static string GetElementValue(AutomationElement element)
        {
            try
            {
                var valuePattern = element.GetCurrentPattern(ValuePattern.Pattern) as ValuePattern;
                if (valuePattern != null)
                {
                    string selectedValue = valuePattern.Current.Value;
                    return selectedValue;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("无法获取ValuePattern: " + ex.Message);
            }
            return "";
        }

        //获取列表类控件的值 ,如ComboBox
        private static string GetElementSelectd(AutomationElement element)
        {
            try
            {
                var selectionPattern = element.GetCurrentPattern(SelectionPattern.Pattern) as SelectionPattern;
                if (selectionPattern != null)
                {
                    AutomationElement selectedItem = selectionPattern.Current.GetSelection().FirstOrDefault();
                    if (selectedItem != null)
                    {
                        string selectedValue = selectedItem.GetCurrentPropertyValue(AutomationElement.NameProperty) as string;
                        return selectedValue;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("无法获取SelectionPattern: " + ex.Message);
            }
            return "";
        }
        #endregion
        #endregion

        #region 转换
        /// <summary>
        /// 转换区域坐标
        /// </summary>
        /// <param name="rect"></param>
        /// <returns></returns>
        public static Rectangle ConvertWindowRectToRectangle(System.Windows.Rect rect)
        {
            Rectangle newRect = new Rectangle((int)rect.X, (int)rect.Y, (int)rect.Width, (int)rect.Height);
            return newRect;
        }

        /// <summary>
        /// 转换控件类型
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        public static ControlType ConvertToControlType(string typeName)
        {
            ControlType controlType = null;
            switch (typeName)
            {
                case "ControlType.Button": controlType = ControlType.Button; break;
                case "ControlType.Tree": controlType = ControlType.Tree; break;
                case "ControlType.TreeItem": controlType = ControlType.TreeItem; break;
                case "ControlType.Custom": controlType = ControlType.Custom; break;
                case "ControlType.Group": controlType = ControlType.Group; break;
                case "ControlType.Thumb": controlType = ControlType.Thumb; break;
                case "ControlType.DataGrid": controlType = ControlType.DataGrid; break;
                case "ControlType.DataItem": controlType = ControlType.DataItem; break;
                case "ControlType.ToolTip": controlType = ControlType.ToolTip; break;
                case "ControlType.Document": controlType = ControlType.Document; break;
                case "ControlType.Window": controlType = ControlType.Window; break;
                case "ControlType.Pane": controlType = ControlType.Pane; break;
                case "ControlType.Header": controlType = ControlType.Header; break;
                case "ControlType.HeaderItem": controlType = ControlType.HeaderItem; break;
                case "ControlType.Table": controlType = ControlType.Table; break;
                case "ControlType.TitleBar": controlType = ControlType.TitleBar; break;
                case "ControlType.Separator": controlType = ControlType.Separator; break;
                case "ControlType.SplitButton": controlType = ControlType.SplitButton; break;
                case "ControlType.Text": controlType = ControlType.Text; break;
                case "ControlType.ToolBar": controlType = ControlType.ToolBar; break;
                case "ControlType.Tab": controlType = ControlType.Tab; break;
                case "ControlType.Calendar": controlType = ControlType.Calendar; break;
                case "ControlType.CheckBox": controlType = ControlType.CheckBox; break;
                case "ControlType.ComboBox": controlType = ControlType.ComboBox; break;
                case "ControlType.Edit": controlType = ControlType.Edit; break;
                case "ControlType.Hyperlink": controlType = ControlType.Hyperlink; break;
                case "ControlType.Image": controlType = ControlType.Image; break;
                case "ControlType.ListItem": controlType = ControlType.ListItem; break;
                case "ControlType.TabItem": controlType = ControlType.TabItem; break;
                case "ControlType.Menu": controlType = ControlType.Menu; break;
                case "ControlType.List": controlType = ControlType.List; break;
                case "ControlType.MenuItem": controlType = ControlType.MenuItem; break;
                case "ControlType.ProgressBar": controlType = ControlType.ProgressBar; break;
                case "ControlType.RadioButton": controlType = ControlType.RadioButton; break;
                case "ControlType.ScrollBar": controlType = ControlType.ScrollBar; break;
                case "ControlType.Slider": controlType = ControlType.Slider; break;
                case "ControlType.Spinner": controlType = ControlType.Spinner; break;
                case "ControlType.StatusBar": controlType = ControlType.StatusBar; break;
                case "ControlType.MenuBar": controlType = ControlType.MenuBar; break;
                default: controlType = ControlType.Text; break;
            }
            return controlType;
        }
        #endregion
    }
}
