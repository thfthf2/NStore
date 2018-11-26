using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NStore.Core.Domain.Product
{
    /// <summary>
    /// 专场信息类
    /// </summary>
    public class SpecialPerformanceInfo
    {
        private int _specialid;//专场id
        private string _name = "";//专场名称
        private int _sort = 0;//专场排序
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
        /// 专场id
        /// </summary>
        public string Name
        {
            set { _name = value; }
            get { return _name; }
        }

        /// <summary>
        /// 专场id
        /// </summary>
        public int Sort
        {
            set { _sort = value; }
            get { return _sort; }
        }

        /// <summary>
        /// 专场id
        /// </summary>
        public int State
        {
            set { _state = value; }
            get { return _state; }
        }

    }
}
