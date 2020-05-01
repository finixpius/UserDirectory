using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserDirectory.Model;

namespace UserDirectory.ViewModel
{
    public class UserListViewModel : INotifyPropertyChanged
    {
        #region Properties
        public event PropertyChangedEventHandler PropertyChanged;

        private clsUser selectedUser;

        public clsUser SelectedUser
        {
            get { return selectedUser; }
            set
            {
                selectedUser = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedUser"));
            }
        }

        private List<clsUser> lstUser;
        public List<clsUser> LstUser
        {
            get { return lstUser; }
            set
            {
                lstUser = value;

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LstUser"));
            }
        }
        #endregion

        #region Ctor
        public UserListViewModel()
        {
        }
        #endregion

        #region Public Methods
        public void Initialize()
        {
            LstUser = GetAllUser();
            SelectedUser = LstUser.FirstOrDefault();
        }
        public List<clsUser> GetAllUser()
        {
            string strQry = "select * from tblUser with (nolock)";
            DataTable dtUser = GetUser(strQry);
            List<clsUser> lstUser = new List<clsUser>();

            if (dtUser != null && dtUser.Rows.Count != 0)
            {
                clsUser objUser = null;
                foreach (DataRow drUser in dtUser.Rows)
                {
                    objUser = new clsUser()
                    {
                        ID = drUser["ID"] != DBNull.Value ? Convert.ToInt32(drUser["ID"]) : 0,
                        FirstName = drUser["FirstName"] != DBNull.Value ? Convert.ToString(drUser["FirstName"]) : string.Empty,
                        LastName = drUser["LastName"] != DBNull.Value ? Convert.ToString(drUser["LastName"]) : string.Empty,
                        EmailAddress = drUser["emailaddress"] != DBNull.Value ? Convert.ToString(drUser["emailaddress"]) : string.Empty,
                        Gender = drUser["gender"] != DBNull.Value ? Convert.ToInt32(drUser["gender"]) : 0
                    };
                    lstUser.Add(objUser);
                }

            }
            return lstUser;
        }

        public clsUser GetUser(int userID)
        {
            string strQry = string.Format("select * from tblUser with (nolock) where ID={0}", userID);
            DataTable dtUser = GetUser(strQry);
            clsUser objUser = null;
            if (dtUser != null && dtUser.Rows.Count != 0)
            {
                DataRow drUser = dtUser.Rows[0];
                {
                    objUser = new clsUser()
                    {
                        ID = drUser["ID"] != DBNull.Value ? Convert.ToInt32(drUser["ID"]) : 0,
                        FirstName = drUser["FirstName"] != DBNull.Value ? Convert.ToString(drUser["FirstName"]) : string.Empty,
                        LastName = drUser["LastName"] != DBNull.Value ? Convert.ToString(drUser["LastName"]) : string.Empty,
                        EmailAddress = drUser["emailaddress"] != DBNull.Value ? Convert.ToString(drUser["emailaddress"]) : string.Empty,
                        Gender = drUser["gender"] != DBNull.Value ? Convert.ToInt32(drUser["gender"]) : 0
                    };
                }
            }
            return objUser;
        }
        #endregion

        #region Private Methods
        private DataTable GetUser(string strQry)
        {
            if (string.IsNullOrEmpty(strQry))
                return null;

            return Data.DBConnectionManager.Instance.GetData(strQry);
        } 
        #endregion
    }
}
