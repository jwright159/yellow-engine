using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WrightWay.YellowVR
{
    /// <summary>
    /// Standard <see cref="ElementalInterface"/>s that simulate transferring different types of energy. Largely unfinished, and should probably be replaced with static variables.
    /// </summary>
    [RequireComponent(typeof(Rigidbody))]
    public class EnergyTransferer : MonoBehaviour
    {
        public ElementalInterface fireInterface;
        public ElementalInterface manaInterface;
        public ElementalInterface soulInterface;

        private Dictionary<Element, ElementalInterface> interfaces;

        [NonSerialized]
        public new Rigidbody rigidbody;

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody>();
            interfaces = new Dictionary<Element, ElementalInterface>();
            interfaces.Add(fireInterface.element, fireInterface);
            interfaces.Add(manaInterface.element, manaInterface);
            interfaces.Add(soulInterface.element, soulInterface);
        }

        // in Joules ig
        public void Absorb(Element element, float energy)
        {
            ElementalInterface inter = interfaces[element];
            inter.amount += energy / inter.capacity / rigidbody.mass;
        }

        // Returns vector with direction as velocity and magnitude as energy
        // probably???
        public Vector3 GetKineticEnergy()
        {
            return rigidbody.mass * rigidbody.velocity.sqrMagnitude * 0.5f * rigidbody.velocity.normalized;
        }

        /// <summary>
        /// Data structure for an <see cref="Element"/>.
        /// </summary>
        [Serializable]
        public class ElementalInterface
        {
            public Element element;
            public float amount;
            public float capacity; // in J/(kg unit)

            public ElementalInterface(Element element, float amount, float capacity)
            {
                this.element = element;
                this.amount = amount;
                this.capacity = capacity;
            }
        }
    }
}