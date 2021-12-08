namespace CO3402_Assignment
{
    public class Stack<T>
    {
        protected class Element
        {
            public T value;                         // Stored element
            public Element next;                 // Linked element

            // Constructors
            public Element(T elementValue)
            {
                value = elementValue;
                next = null;
            }
            public Element(T element, Element nextElement)
            {
                value = element;
                next = nextElement;
            }
        };

        protected Element Top;
        public Stack(T element)
        {
            Top = new Element(element);
        }

        public Stack()
        {
            Top = null;
        }

        public virtual void push(T newElement)
        {
            Top = new Element(newElement, Top);
        }

        public virtual T top()
        {
            if (isEmpty())
            {
                return default(T);
            }

            return Top.value;
        }

        public virtual T pop()
        {
            if (isEmpty())
            {
                return default(T);
            }

            Element oldTop = Top;        // Keep track of current top
            Top = Top.next;                 // Make the next element the new top

            T poppedElement = oldTop.value; // Keep track of the removed element

            return poppedElement;           // Return value of removed element
        }

        public int size()
        {
            Element t = Top;
            int count = 0;

            while (t != null)
            {
                count++;
                t = t.next;
            }

            return count;
        }

        public bool isEmpty()
        {
            return Top == null;
        }

        public virtual string ToString(string separator)
        {
            Element t = Top;
            string s = "";

            while (t != null)
            {
                s += t.value;
                s += separator;
                t = t.next;
            }

            return s;
        }

        public override string ToString()
        {
            return ToString(" ");
        }
    }
}
