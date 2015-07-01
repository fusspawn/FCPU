using Parser.FuLang.Tokens;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Parser.FuLang
{
    public class Tokenizer
    {
        List<Token> TokenList;
        StreamReader FileReader;
        char CurrentChar;

        public List<Token> TokenizeSource(string Filepath) {
            TokenList = new List<Token>();

            CheckFile(Filepath);
            Processfile(Filepath);

            return TokenList;    
        }

        private void Processfile(string filepath)
        {
            TokenList.Clear();
            FileReader = new StreamReader(File.OpenRead(filepath));
            ReadChar();

            do
            {
                TokenList.Add(GetNextToken());
            } while (!FileReader.EndOfStream);
        }

        private void CheckFile(string filepath)
        {
            if (!File.Exists(filepath))
                throw new ArgumentException("Invalid filepath: " + filepath);
        }

        private Token GetNextToken() {
            while (char.IsWhiteSpace(CurrentChar))
            {
                ReadChar();
                if (FileReader.EndOfStream)
                    return new Token(TokenType.EOF);
            }

            switch (CurrentChar) {
                case '+':
                    ReadChar();
                    return new Token(TokenType.Addition);
                case '-':
                    ReadChar();
                    return new Token(TokenType.Subtract);
                case '/':
                    ReadChar();
                    return new Token(TokenType.Divide);
                case '*':
                    ReadChar();
                    return new Token(TokenType.Mulitply);
                case '(':
                    ReadChar();
                    return new Token(TokenType.LParen);
                case ')':
                    ReadChar();
                    return new Token(TokenType.RParen);
                case '=':
                    ReadChar();
                    return new Token(TokenType.Assign);
                case ';':
                    ReadChar();
                    return new Token(TokenType.SemiColon);
                case '{':
                    ReadChar();
                    return new Token(TokenType.LBracket);
                case '}':
                    ReadChar();
                    return new Token(TokenType.RBracket);
                default:
                    if (char.IsDigit(CurrentChar))
                        return GetDigit();
                    if (char.IsLetterOrDigit(CurrentChar))
                        return GetIdent();
                    throw new InvalidProgramException("Tokenizer Error");
            }
        }

        private Token GetDigit()
        {
            string NumString = "";
            NumString += CurrentChar;
            ReadChar();

            while (char.IsDigit(CurrentChar) && !char.IsWhiteSpace(CurrentChar))
            {
                NumString += CurrentChar;
                ReadChar();
            }

            return new Token(TokenType.Digit, NumString);            
        }

        private Token GetIdent()
        {
            string IdentString = "";
            IdentString += CurrentChar;
            ReadChar();

            while (char.IsLetterOrDigit(CurrentChar) && !char.IsWhiteSpace(CurrentChar))
            {
                IdentString += CurrentChar;
                ReadChar();
            }

            if (IdentString == "function") {
                return new Token(TokenType.FunctionDef);
            }

            return new Token(TokenType.Ident, IdentString);
        }

        private void ReadChar() {
            CurrentChar = (char)FileReader.Read();
            Console.WriteLine(CurrentChar);
        }
    }
}
