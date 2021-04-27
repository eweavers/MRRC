using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace MRRC
{
   
    //_________________________________________________  
    //This code is from Shlomo Geva's lecture in week 9
    //__________________________________________________
    public class RPN
    {
        ArrayList operators;
        public ArrayList Infix { get; } = new ArrayList();
        public ArrayList Postfix { get; } = new ArrayList();

        public RPN(ArrayList query)
        {
            // define valid tokens
            operators = new ArrayList();
            // here we hardcode opernads and operators; this can be done in a more elegant manner.
            string[] t1 = { "AND", "OR", "(", ")" }; // operators and parentheses
            operators.AddRange(t1);
            // Create and instantiate a new empty Stack.
            Stack rpnStack = new Stack();

            // apply dijkstra algorithm using a stack to convert infix to postfix notation (=rpn)
            Infix.AddRange(query);
            foreach (string token in Infix)
            {
                if (token.Equals("("))
                {   // push open parenthesis onto stack
                    rpnStack.Push(token);
                }
                else if (token.Equals(")"))
                {   // pop all operators off the stack until the mathcing open parenthesis is found
                    while ((rpnStack.Count > 0) && !((string)rpnStack.Peek()).Equals("("))
                    {
                        Postfix.Add(rpnStack.Pop());  // transfer operator to output
                        if (rpnStack.Count == 0)
                            throw new Exception("Ubalanced parenthesis");
                    }
                    if (rpnStack.Count == 0)
                        throw new Exception("Ubalanced parenthesis");
                    rpnStack.Pop(); // discard open parenthesis
                }
                else if (operators.Contains(token))
                {   // push operand to the rpn stack after moving to output all higher or equal priority operators
                    while (rpnStack.Count > 0 && ((string)rpnStack.Peek()).Equals("AND"))
                    {
                        Postfix.Add(rpnStack.Pop());  // pop and add to output
                    }
                    rpnStack.Push(token); // now pus the operator onto the stack
                }
                else
                {
                    Postfix.Add(token);
                }
                // throw new Exception("Unrecognised token " + token);
            }
            // now copy what's left on the rpnStack
            while (rpnStack.Count > 0)
            {   // move to the output all remaining operators
                if (((string)rpnStack.Peek()).Equals("("))
                    throw new Exception("Ubalanced parenthesis");
                Postfix.Add(rpnStack.Pop());
            }
        } // end RPN() constructor

    } //end Class RPN


}
