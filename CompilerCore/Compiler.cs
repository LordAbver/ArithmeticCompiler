using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ArithmeticCompiler.CompilerCore
{
    class Compiler
    {
        #region Private fields

        private readonly Statement _currentStatement;

        #endregion

        #region Constructor

        public Compiler()
        {
            _currentStatement=new Statement();
        }

        #endregion

        #region Public Methods

        public void Compile(String statement)
        {
            statement = String.Format("({0})", statement);
            _currentStatement.Clear();
            Int32 index = 0;
            Object lexem = null;
            Object previousLexem=Char.MinValue;
            do
            {
                if (index < statement.Length && Char.IsWhiteSpace(statement[index]))
                {
                    index++;
                    continue; //Ignore spaces
                }
                lexem = ProcessCurrentChar(statement, ref index);
                if (lexem is Char && previousLexem is Char &&
                   (Char)previousLexem == '(' &&
                   ((Char)lexem == '+' || (Char)lexem == '-'))
                    _currentStatement.Operands.Push(0); 

                if (lexem is Int32) //Operand
                {
                    _currentStatement.Operands.Push((Int32)lexem); 
                }
                else if (lexem is char) // Operator
                {
                    if ((char)lexem == ')')
                    {
                        while (_currentStatement.Operators.Count > 0 && _currentStatement.Operators.Peek() != '(')
                           _currentStatement.Pop();
                           _currentStatement.Operators.Pop(); 
                    }
                    else
                    {
                        while (_currentStatement.IsPopPossible((char)lexem))
                            _currentStatement.Pop();

                        _currentStatement.Operators.Push((char)lexem); 
                    }
                }
                previousLexem = lexem;

            } while (lexem != null);

            _currentStatement.Validate();
        }

        public Double GetResult()
        {
          return _currentStatement.GetResult(); 
        }

        #endregion

        #region Private Methods
       
        private static Object ProcessCurrentChar(String statement, ref int index)
        {    
            if (index == statement.Length)
                return null;

            if (Char.IsDigit(statement[index]))
                return ParseOperand(statement, ref index);

            return statement[index++];
        }

        private static Int32 ParseOperand(String statement, ref int index)
        {
            var res = String.Empty;
            while (index < statement.Length && (char.IsDigit(statement[index])))
                res += statement[index++];
            return Convert.ToInt32(res);
        }

        #endregion
    }
}
