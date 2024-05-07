using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Events;

public class CharacterEvents// 유니티 이벤트 들
{
    public static UnityAction<GameObject, int> characterDamaged;

    public static UnityAction<GameObject, int> characterHealed;
}
 
