using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using TextEditor.Extensions;

namespace TextEditor.Engine
{
    public class RegexConverter
    {
        //Oluşturulan postfix değeri tutulur 
        private string _postfixRegex = string.Empty;

        // Dönüştürülecek infix regexi tutulur
        private string _infixRegex = string.Empty;

        // Infixten postfixe çevrilirken anlık karakter değeri
        private char _currentCharacter = Char.MinValue;

        // Infixten postfixe çevrilirken anlık karakter indexi
        private int _index = 0;

        // Oluşturulan postfix değerini döner
        public string PostfixRegex => _postfixRegex;

        /// <summary>
        /// Infix regex i postfixe çevirir
        /// </summary>
        /// <returns>Oluşturulan postfix değerini döner</returns>
        public string ConvertToPostfix()
        {
            // Postfix değerini sıfırlar
            var postFix = string.Empty;
            NextCharacter();
            OrOperatorCheck(ref postFix);
            return postFix;
        }

        /// <summary>
        /// Yeni infix değerini atar ve değişkenleri sıfırlayıp postfix değerini çevirir
        /// </summary>
        /// <param name="infixRegex"></param>
        public void SetInfixRegex(string infixRegex)
        {
            // Tekrar postfixe çevirmemek için eğer yei gelen infix önceki ile aynıysa işlemleri tekrar etmez.
            if (infixRegex == _infixRegex) return;
            _infixRegex = infixRegex + Char.MinValue;
            _index = 0;
            _currentCharacter = Char.MinValue;
            _postfixRegex = ConvertToPostfix();
        }

        private void OrOperatorCheck(ref string postFix)
        {
            NormalCharactersCheck(ref postFix);
            while (_currentCharacter == '|')
            {
                NextCharacter();
                NormalCharactersCheck(ref postFix);
                postFix += "|";
            }
        }

        private void NormalCharactersCheck(ref string postFix)
        {
            SpecialCharactersCheck(ref postFix);
            while (_currentCharacter != Char.MinValue && !")|*+?".Contains(_currentCharacter))
            {
                SpecialCharactersCheck(ref postFix);
                postFix += "&";
            }
        }

        private void SpecialCharactersCheck(ref string postFix)
        {
            GeneralCheck(ref postFix);
            while (_currentCharacter != Char.MinValue && "*+?".Contains(_currentCharacter))
            {
                postFix += _currentCharacter;
                NextCharacter();
            }
        }

        private void GeneralCheck(ref string postFix)
        {
            if (_currentCharacter == Char.MinValue)
                throw new Exception("Regural Expression Incorrect Format");

            if (_currentCharacter == '\\')
            {
                NextCharacter();
                if (_currentCharacter == Char.MinValue)
                    throw new Exception("Regural Expression Incorrect Format");
                postFix += _currentCharacter;
                NextCharacter();
            }
            else if (!"()|*+?".Contains(_currentCharacter))
            {
                postFix += _currentCharacter;
                NextCharacter();
            }
            else if (_currentCharacter == '(')
            {
                NextCharacter();
                OrOperatorCheck(ref postFix);

                if (_currentCharacter != ')')
                    throw new Exception("Regural Expression Incorrect Format");

                NextCharacter();
            }
            else
                throw new Exception("Regural Expression Incorrect Format");
        }


        private void NextCharacter()
        {
            _currentCharacter = _infixRegex[_index++];
        }
    }
}
