using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

public static class NodeExternal
{
    public static void AddNode(this INode nod, ISubject sub)
    {
        sub.eventArray += nod.ExcuteMethod;
    }

    public static void RemoveNode(this INode nod, ISubject sub)
    {
        sub.eventArray -= nod.ExcuteMethod;
    }
 
    public static void Excute(this INode sender, EventArgs e)
    {
        sender.ExcuteMethod(sender, e);
    }

    public static void ExcuteMethod(this INode nod, object sender, EventArgs e)
    {
        ISubject sub = nod as ISubject;

        if (sub != null)
        {
            sub.NotifyObservers(e);
        }
        
        NodeEventArgs eventdata = e as NodeEventArgs;

        MethodInfo mInfo = nod.GetType().GetMethod(eventdata.eventName,
                                                     BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public
                                                   );
        if (mInfo != null)
        {
            eventdata.result = mInfo.Invoke(nod, eventdata.data);
        }
    }
}