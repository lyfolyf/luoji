using Cognex.VisionPro.CalibFix;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Encap
{
    [Serializable]
    public class CalibFile
    {
        /// <summary>
        /// 标定工具CheckBoardTool集合
        /// </summary>
        public List<CogCalibCheckerboardTool> mCBTs;

        public CalibFile(){}
        public CalibFile(List<CogCalibCheckerboardTool> ToolsList)
        {
            mCBTs = ToolsList;
        }
    }
}
