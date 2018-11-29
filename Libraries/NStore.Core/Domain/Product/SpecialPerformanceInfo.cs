using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStore.Core
{
    /// <summary>
    /// 专场信息类
    /// </summary>
    public class SpecialPerformanceInfo
    {
        private int _specialid;//专场id
        private string _name = "";//专场名称
        private int _displayorder = 0;//排序
        private int _state = 0;//状态


        /// <summary>
        /// 专场id
        /// </summary>
        public int Specialid
        {
            set { _specialid = value; }
            get { return _specialid; }
        }
        
        /// <summary>
        /// 专场名称
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }

        /// <summary>
        /// 排序
        /// </summary>
        public int DisplayOrder
        {
            set { _displayorder = value; }
            get { return _displayorder; }
        }

        /// <summary>
        /// 状态
        /// </summary>
        public int State
        {
            set { _state = value; }
            get { return _state; }
        }

    }
}
