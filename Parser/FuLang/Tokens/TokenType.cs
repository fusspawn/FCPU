using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parser.FuLang.Tokens
{
    public enum TokenType {
        Unknown,
        Digit,
        Character,
        Ident,
        Addition,
        Mulitply,
        Divide,
        Subtract,
        RParen,
        LParen,
        LBracket,
        RBracket,
        Assign,
        SemiColon,
        FunctionDef,
        EOF
    }
}
