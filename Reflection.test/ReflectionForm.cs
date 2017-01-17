using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;


namespace Reflection
{
    /*
     * 리플렉션
     * https://msdn.microsoft.com/ko-kr/library/ms173183(v=VS.90).aspx
     */
    class ReflectionForm
    {
        /*
         * 중복허용
         */
        bool __duplicate = true;
        /*
         * 이미 열려있는 폼이 있다면, 활성화
         */
        bool __activate = false;

        public enum Options
        {
            NoDuplicate = 1
            , Activate = 1 * 2
        }

        public ReflectionForm()
        {
            __duplicate = true; //중복허용 기본값
            __activate = false;  //활성화안함 기본값 
        }

        public ReflectionForm(Options option)
        {
            if ((option & Options.NoDuplicate) != 0)
                __duplicate = false;
            if ((option & Options.Activate) != 0)
                __activate = true;
        }

        

        string makeName(string nameSpace, string className)
        {
            string name = string.Empty;

            return name = string.Format(
                (nameSpace.Length > 0) ? "{0}.{1}" : "{1}"
                , nameSpace
                , className);
        }

        Form GetFormInstance(
              Assembly o
            , string assamblyName)
        {

            Type type = o.GetType(assamblyName);

            /**/
            var list = o.DefinedTypes.ToList();


            Debug.Write(string.Format("{0,10} {1} \n", "(" + list.Count + ")", o.FullName.Split(',')[0]));

            foreach (var it in list)
            {
                //if (!it.Equals(list.Last()))
                    Debug.Write(string.Format("{0,10} {1}\n","", it.FullName));
                //else
                //    Debug.Write(string.Format("{0}", it.FullName));
            }

            Debug.Write("\n");

            /**/
            Form frm = o.CreateInstance(
                  type.FullName) as Form;

            //if (frm != null) if (parent != null) frm.Parent = parent;

            return frm;
        }

        public Form GetFormInstance(
              string nameSpace
            , string className
            )
        {
            //중복검사
            if (!(GetOpenForm(className) == null))
                return GetOpenForm(className);

            Assembly o = Assembly.GetExecutingAssembly();
                        
            return GetFormInstance(o, makeName(nameSpace, className));

        }

        public Form GetFormInstance(
           string className
         )
        {
            //중복검사
            if (!(GetOpenForm(className) == null))
                return GetOpenForm(className);

            return GetFormInstance(string.Empty, className);
        }

        /*
         * DLL에서 정의한 폼의 인스턴스를 가져오기위한
         * 
         */
        public Form GetDllFormInstance(
              string dllName 
            , string nameSpace
            , string className)
        {
            if (!(GetOpenForm(className) == null))
                return GetOpenForm(className);

            try
            {
                //DLL을 이름으로 로드하여 어셈블리를 가져온다
                //이때 이름은 FULL_PATH
                Assembly o = Assembly.LoadFrom(dllName);

                return GetFormInstance(o, makeName(nameSpace, className));
            }
            catch
            {
                return null;
            }
            
        }
        
        public Form GetOpenForm(string className)
        {
            Form o = null;

            //중복허용이면 루프를 돌지않고, 
            //null을 리턴하여 생성할 수 있게한다.
            if (!__duplicate)
            {
                //이미 오픈되어있는 폼이 있는지 
                //실행중인 어플리케이션의 폼 리스트에서 찾는다.
                foreach (Form frm in Application.OpenForms)
                    if (className == frm.Name)
                    {
                        o = frm; //찾았다
                        if (__activate) o.Activate();
                    }
            }
            //못 찾았다
            return o;

        }



    }
}
