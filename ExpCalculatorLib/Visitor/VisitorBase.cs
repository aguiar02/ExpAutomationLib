using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExpCalculatorLib.Expression;

namespace ExpCalculatorLib.Visitor
{
    public class VisitorBase
    {
        public virtual bool Visit(IExpression exp)
        {
            return true;
        }

        /// <summary>
        /// Visits the expression and returns whether the visitor must visit subexpressions
        /// </summary>
        /// <param name="exp"></param>
        /// <returns>true if the visitor must visit subexpression and false otherwise</returns>
        public virtual bool VisitEnter(IExpression exp)
        {
            return true;
        }

        public virtual bool VisitLeave(IExpression exp)
        {
            return true;
        }
    }
}
