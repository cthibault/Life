using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MVVM.Life.Common.Models;

namespace MVVM.Life.Business.Services
{
    public class BasicRulesManager : RulesManager
    {
        public BasicRulesManager()
        {
            List<IRule> basicRules = new List<IRule>();
            basicRules.Add(new Rule("Under Populated", 
                                    (cell, context) => 
                                    {
                                        if (cell.Health > 0)
                                            return context.Neighbors.Count(n => n.Health > 0) < 2;
                                        return false;
                                    },
                                    h => { return 0; }));
            basicRules.Add(new Rule("Perfect Condition", 
                                    (cell, context) => 
                                    {
                                        int alive = context.Neighbors.Count(n => n.Health > 0);
                                        if (cell.Health > 0)
                                            return ((alive == 2) || (alive == 3));
                                        return false;
                                    },
                                    h => { return 100; }));
            basicRules.Add(new Rule("Over Populated", 
                                    (cell, context) => 
                                    {
                                        if (cell.Health > 0)
                                            return context.Neighbors.Count(n => n.Health > 0) > 3;
                                        return false;
                                    },
                                    h => { return h - 50M; }));
            basicRules.Add(new Rule("Rebirth", 
                                    (cell, context) => 
                                    {
                                        if (cell.Health == 0)
                                            return context.Neighbors.Count(n => n.Health > 0) == 3;
                                        return false;
                                    },
                                    h => { return 100; }));

            Rules = basicRules;
        }
    }
}
