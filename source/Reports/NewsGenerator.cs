using System.Collections;

namespace OOD_24L_01180686.source.Reports
{
    public class NewsGenerator : IEnumerable<string>
    {
        public LinkedList<IReportable> Reportables { get; set; }
        public LinkedList<Reporter> Reporters { get; set; }

        public NewsGenerator(LinkedList<Reporter> reporters, LinkedList<IReportable> reportables)
        {
            Reporters = reporters;
            Reportables = reportables;
        }

        public IEnumerator<string> GetEnumerator()
        {
            return new NewsIterator(Reporters, Reportables);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }


    public class NewsIterator : IEnumerator<string>
    {
        private LinkedListNode<Reporter>? reporterNode;
        private LinkedListNode<IReportable>? reportableNode;

        private LinkedList<Reporter>? reporters;
        private LinkedList<IReportable>? reportables;

        public NewsIterator(LinkedList<Reporter> reporters, LinkedList<IReportable> reportables)
        {
            this.reporters = reporters;
            this.reportables = reportables;
            Reset();
        }

        public string Current { get; private set; }
        object IEnumerator.Current => Current;

        public void Dispose()
        {
        }

        public bool MoveNext()
        {
            if (reporterNode == null || reportableNode == null)
                return false;

            Current = reportableNode.Value.Accept(reporterNode.Value);

            reportableNode = reportableNode.Next;
            if (reportableNode == null)
            {
                reporterNode = reporterNode.Next;
                reportableNode = reportables.First;
            }

            return true;
        }

        public void Reset()
        {
            reporterNode = reporters.First;
            reportableNode = reportables.First;
            Current = null;
        }
    }
}