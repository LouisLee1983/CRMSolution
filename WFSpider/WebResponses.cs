using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WFSpider
{

    public class AgentGradeOperationRoot
    {
        public bool ret { get; set; }
        public string errmsg { get; set; }
        public AgentGradeOperationData data { get; set; }
    }

    public class AgentGradeOperationData
    {
        public int totalCount { get; set; }
        public int totalPage { get; set; }
        public int currentPageNo { get; set; }
        public int pageSize { get; set; }
        public AgentDetailOperationDatum[] data { get; set; }
    }
    
    public class AgentDetailOperationDatum
    {
        public int id { get; set; }
        public string agentDomain { get; set; }
        public string agentName { get; set; }
        public string promotion { get; set; }
        public string agentManager { get; set; }
        public int totalTicketNum { get; set; }
        public decimal? totalTicket { get; set; }
        public decimal? passRate { get; set; }
        public decimal? less60minRate { get; set; }
        public decimal? orderAlterRate { get; set; }
        public decimal? voluntaryRate { get; set; }
        public decimal? involuntaryRate { get; set; }
        public decimal? complainRate { get; set; }
        public decimal? qapassRate { get; set; }
        public decimal? phoneAnswerRate { get; set; }
        public decimal? messageTimeoutRate { get; set; }
        public decimal? qualification { get; set; }
        public decimal? whiteList { get; set; }
        public decimal? totalScore { get; set; }
        public int status { get; set; }
        public string statDate { get; set; }
        public string statMonth { get; set; }
        public string grade { get; set; }
    }

}
