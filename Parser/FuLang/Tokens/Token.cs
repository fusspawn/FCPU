using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parser.FuLang.Tokens
{
    public class Token
    {
        public TokenType TokenType;
        public object TokenValue;

        public Token(TokenType Type, object Value = null) {
            TokenType = Type;
            TokenValue = Value;

        }
    }
}
