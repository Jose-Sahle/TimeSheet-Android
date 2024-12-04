using  System;

namespace SHL.Types
{
    public class IRetornoException : Exception
    {
        private DateTime m_dt;
        private String m_trace;
        private String m_status;
        
        public IRetornoException()
        {
        }

        public IRetornoException(string message) : base(message)
        {
        }

        public IRetornoException(string message, Exception inner) : base(message, inner)
        {
        }
        
        public DateTime Dt 
        {
            get { return m_dt; }
            set { m_dt = value; }
            
        }

        public String Trace
        {
            get { return m_trace; }
            set { m_trace = value; }
        }

        public String Status
        {
            get { return m_status; }
            set { m_status = value; }
        }
    }
}