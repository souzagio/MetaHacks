using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaHacks
{
    class Games
    {
        //---------------------- Saint Seiya Soilder's Soul ---------------------------------------//
        // Automático de 7Sen e Cosmo
        private static Int16 _auto;
        public static Int16 auto
        {
            get { return _auto; }
            set { _auto = value; }
        }
        //---------------------------------------------------------------------------------------//
        //---------------------- Tela de Login e Perfil-------------------------------------------//
        //ID do Usuário//
        private static Int16 _ID;
        public static Int16 ID
        {
            get { return _ID; }
            set { _ID = value; }
        }
        private static DateTime _valdata;
        public static DateTime valdata
        {
            get { return _valdata; }
            set { _valdata = value; }
        }
        private static Int32 _CodigoConta;
        public static Int32 CodigoConta
        {
            get { return _CodigoConta; }
            set { _CodigoConta = value; }
        }
        //---------------------------------------------------------------------------------------//
        //---------------------------------------------------------------------------------------//
    }
}
