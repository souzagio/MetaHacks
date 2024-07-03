using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MetaHacks
{
    class Float
    {
        private static Int16 _login = 0;
        public static Int16 login
        {
            get { return _login; }
            set { _login = value; }
        }
        // Validar Conta
        private static bool _valOk = false;
        public static bool valOk
        {
            get { return _valOk; }
            set { _valOk = value; }
        }
        //--------*****-----
        private static Int16 _radio = 1;
        public static Int16 radio
        {
            get { return _radio; }
            set { _radio = value; }
        }
        //--------Painéis do Cyber Sleuth------
        //Painel 1
        private static Int16 _p1 = 0;
        public static Int16 p1
        {
            get { return _p1; }
            set { _p1 = value; }
        }
        //Painel 2
        private static Int16 _p2 = 0;
        public static Int16 p2
        {
            get { return _p2; }
            set { _p2 = value; }
        }
        //Botões
        private static Int16 _b1 = 1;
        public static Int16 b1
        {
            get { return _b1; }
            set { _b1 = value; }
        }
        //Fix 1
        private static bool _f1 = false;
        public static bool f1
        {
            get { return _f1; }
            set { _f1 = value; }
        }
        //Fix 2
        private static bool _f2 = false;
        public static bool f2
        {
            get { return _f2; }
            set { _f2 = value; }
        }
        //Fix 3
        private static bool _f3 = false;
        public static bool f3
        {
            get { return _f3; }
            set { _f3 = value; }
        }
        //Auto Cura SP1
        private static bool _auto1 = false;
        public static bool auto1
        {
            get { return _auto1; }
            set { _auto1 = value; }
        }
        //Auto Cura SP2
        private static bool _auto2 = false;
        public static bool auto2
        {
            get { return _auto2; }
            set { _auto2 = value; }
        }
        //Auto Cura SP3
        private static bool _auto3 = false;
        public static bool auto3
        {
            get { return _auto3; }
            set { _auto3 = value; }
        }
        //Auto Cura HP 1
        private static bool _Hpauto1 = false;
        public static bool Hpauto1
        {
            get { return _Hpauto1; }
            set { _Hpauto1 = value; }
        }
        //Auto Cura HP 2
        private static bool _Hpauto2 = false;
        public static bool Hpauto2
        {
            get { return _Hpauto2; }
            set { _Hpauto2 = value; }
        }
        //Auto Cura HP 1
        private static bool _Hpauto3 = false;
        public static bool Hpauto3
        {
            get { return _Hpauto3; }
            set { _Hpauto3 = value; }
        }
        //Controle do HP in World 1
        private static bool _HPAuto1 = false;
        public static bool HPAuto1
        {
            get { return _HPAuto1; }
            set { _HPAuto1 = value; }
        }
        //Controle do HP in World 2
        private static bool _HPAuto2 = false;
        public static bool HPAuto2
        {
            get { return _HPAuto2; }
            set { _HPAuto2 = value; }
        }
        //Controle do HP in World 3
        private static bool _HPAuto3 = false;
        public static bool HPAuto3
        {
            get { return _HPAuto3; }
            set { _HPAuto3 = value; }
        }
        //Controle do HP em Batalha
        private static bool _bHp1;
        public static bool bHp1
        {
            get { return _bHp1; }
            set { _bHp1 = value; }
        }
        private static bool _bHp2;
        public static bool bHp2
        {
            get { return _bHp2; }
            set { _bHp2 = value; }
        }
        private static bool _bHp3;
        public static bool bHp3
        {
            get { return _bHp3; }
            set { _bHp3 = value; }
        }
        //Taxa Crítica 1
        private static int _Ct1 = 0;
        public static int Ct1
        {
            get { return _Ct1; }
            set { _Ct1 = value; }
        }
        //Taxa Crítica 2
        private static int _Ct2 = 0;
        public static int Ct2
        {
            get { return _Ct2; }
            set { _Ct2 = value; }
        }
        //Taxa Crítica 3
        private static int _Ct3 = 0;
        public static int Ct3
        {
            get { return _Ct3; }
            set { _Ct3 = value; }
        }
    }
}
