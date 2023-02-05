using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface IChargeCollidable
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    void Collide(int charge, int senderParentId);

    Type GetType();

    int GetID();

    #endregion

    #region Enums



    #endregion
}
