using System;
using System.Collections.Generic;

namespace ArithmeticCompiler.CompilerCore
{
    #region Statement

    public class Statement
    {
        #region Public fields

        public Stack<Double> Operands { get; private set; }
        public Stack<Char> Operators { get; private set; }

        #endregion
        
        #region Constructor

        public Statement()
        {
            Operands = new Stack<Double>();
            Operators = new Stack<Char>();
        }

        #endregion

        #region public Methods

        public void Pop()
        {
            Double y = Operands.Pop();
            Double x = Operands.Pop();

            switch (Operators.Pop())
            {
                case '+': Operands.Push(x + y);
                    break;
                case '-': Operands.Push(x - y);
                    break;
                case '*': Operands.Push(x * y);
                    break;
                case '/': Operands.Push(x / y);
                    break;
            } 
        }

        public Boolean IsPopPossible(char oper)
        {
            if (Operators.Count == 0)
                return false;

            Int32 x = GetPriority(oper);
            Int32 y = GetPriority(Operators.Peek());

            return x >= 0 && y >= 0 && x >= y; 
        }

        public Double GetResult()
        {
            return Operands.Pop();
        }

        public void Validate()
        {
            if (Operands.Count > 1 || Operators.Count > 0)
                throw new Exception("Compilation error!");
        }

        public void Clear()
        {
            Operands.Clear();
            Operators.Clear();
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Get the priority for supported operators
        /// </summary>
        /// <param name="oper">Current operator</param>
        /// <returns>-1-absolute '(',1 - top, 2-lower priority</returns>
        private static Int32 GetPriority(char oper)
        {
            switch (oper)
            {
                case '(':
                    return -1;
                case '*':
                case '/':
                    return 1;
                case '+':
                case '-':
                    return 2;
                default:
                    throw new Exception("Unsupported operator");
            }
        }

        #endregion
    }

    #endregion


}
