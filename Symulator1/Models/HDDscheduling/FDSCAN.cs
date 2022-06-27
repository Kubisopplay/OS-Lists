using Symulator1.Models.Comparators;
using Symulator1.Models.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Media3D;

namespace Symulator1.Models.HDDscheduling
{
    class FDSCAN : HddAlg
    {
        public FDSCAN(string name, int maxlen) : base(name, maxlen)
        {
        }
        public override int evaluate()
        {
            bool maxset = false;
            bool Right = true;
            KeyValuePair<HDDRequest, List<HDDRequest>> max= new KeyValuePair<HDDRequest, List<HDDRequest>>();
            Dictionary<HDDRequest, List<HDDRequest>> kv = new Dictionary<HDDRequest, List<HDDRequest>>();
            List<HDDRequest> prioritylist = new List<HDDRequest>();
            return AdvEvaluate(delegate (HddAlg alg, List<HDDRequest> queue)
            {
                
                prioritylist.AddRange(List.FindAll(e => e.Realtime && e.EntryTime == CurrentTime));
                if (prioritylist.Count > 0)
                {
                    Dropped = Dropped + prioritylist.RemoveAll(e => e.EntryTime + e.Deadline < CurrentTime);
                }
                prioritylist.ForEach(e =>
                {
                    kv[e] = new List<HDDRequest>();
                    List<HDDRequest> temp = new List<HDDRequest>();
                    var timeleft = e.Deadline + e.EntryTime-e.Duration;
                    prioritylist.ForEach(ee =>
                    {
                        if ((ee.Cylinder < HeadLocation && ee.Cylinder > e.Cylinder) || (ee.Cylinder > HeadLocation && ee.Cylinder < e.Cylinder))
                        {
                            temp.Add(new HDDRequest( ee));
                        }
                    });
                    queue.ForEach(ee =>
                    {
                        if ((ee.Cylinder < HeadLocation && ee.Cylinder > e.Cylinder) || (ee.Cylinder > HeadLocation && ee.Cylinder < e.Cylinder))
                        {
                            temp.Add(new HDDRequest(ee));
                        }
                    });
                    HDDRequest lastitem = new HDDRequest(-1, HeadLocation, -1);
                    temp.Sort((head, o2) =>
                    {
                        return (head.Cylinder - HeadLocation).CompareTo(o2.Cylinder - HeadLocation);
                    });
                    foreach (var item in temp)
                    {
                        if (item.Duration+Math.Abs(item.Cylinder-lastitem.Cylinder) < timeleft)
                        {
                            timeleft -=( item.Duration + Math.Abs(item.Cylinder - lastitem.Cylinder));
                            kv[e].Add(new HDDRequest(item));
                        }
                        lastitem = item;
                    }
                });

            if (kv.Count > 0) {
                    if (!maxset)
                    {
                        int maxi = kv.Max(e => e.Value.Count);
                        {
                            foreach (var item in kv)
                            {
                                if (item.Value.Count == maxi)
                                {
                                    max = item;
                                    maxset = true;
                                    kv.Clear();
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (max.Value!=null&&max.Value.Count > 0)
                        {
                            if (max.Value[0].Cylinder == HeadLocation)
                            {
                                if (max.Value[0].Duration == 0)
                                {
                                    
                                    prioritylist.Remove(max.Value[0]);
                                    queue.Remove(max.Value[0]);
                                    max.Value.RemoveAt(0);
                                }
                                else
                                {
                                    max.Value[0].Duration--;
                                }
                            }
                            else
                            {
                                HeadLocation += Math.Sign(max.Value[0].Cylinder - HeadLocation);
                                HeadTravel++;
                            }
                        }
                        else
                        {
                            if (max.Key!=null&&max.Key.Cylinder == HeadLocation)
                            {

                                if (max.Key.Duration == 0)
                                {
                                    maxset = false;
                                    prioritylist.Remove(max.Key);
                                }
                                else
                                {
                                    max.Key.Duration--;
                                }
                            }
                            else if(max.Key!=null)
                            {
                                HeadLocation += Math.Sign(max.Key.Cylinder - HeadLocation);
                                HeadTravel++;
                            }
                        }

                    }
                }
                    else
                {

                    var normal = queue.Find(e => e.Cylinder == HeadLocation);
                    normal = prioritylist.Find(e => e.Cylinder == HeadLocation) !=null? prioritylist.Find(e => e.Cylinder == HeadLocation) : normal;
                    if (normal != null)
                    {
                        if (normal.Duration == 0)
                        {
                            queue.Remove(normal);
                            prioritylist.Remove(normal);
                        }
                        else
                        {
                            normal.Duration--;

                        }
                    }
                    else
                    {
                        if (Right)
                        {
                            this.HeadLocation++;
                            this.HeadTravel++;
                            if (this.HeadLocation == this.maxblock)
                            {
                                Right = false;
                            }
                        }
                        else
                        {
                            this.HeadLocation--;
                            this.HeadTravel++;
                            if (this.HeadLocation == 0)
                            {
                                Right = true;
                            }
                        }
                    }
                    
                }

                

            });

        }
    }
}
