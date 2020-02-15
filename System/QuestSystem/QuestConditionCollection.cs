﻿using Base2D.System.Generic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base2D.System.QuestSystem
{
    public class QuestConditionCollection : IQuestCondition, IEnumerable
    {
        public event GameEventHandler<IQuestCondition> Completed;

        public int CurrentProgress => _currentProgress;
        public int MaxProgress => Conditions.Count;

        private List<QuestCondition> Conditions;
        private int _currentProgress;
        public QuestConditionCollection()
        {
            _currentProgress = 0;
            Conditions = new List<QuestCondition>();
        }

        public void Add(QuestCondition condition)
        {
            Conditions.Add(condition);
            condition.Completed += ProgressComplete;
        }

        protected void ProgressComplete(IQuestCondition s)
        {
            _currentProgress += 1;
            if (_currentProgress == MaxProgress)
            {
                Completed?.Invoke(this);
                s.Completed -= ProgressComplete;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return Conditions.GetEnumerator();
        }
    }
}
