using System.Collections.Generic;
using agregateurWPF;

namespace agregateurMetier
{
    public class CsgoPageControlleur
    {
        private CsgoPage m_Vue;

        public CsgoPageControlleur(CsgoPage a_Vue)
        {
            m_Vue = a_Vue;
            GenerateUpdatesButton(MainWindowControlleur.Instance.CsGoUpdades);
        }

        private void GenerateUpdatesButton(List<CsGoUpdate> a_Updates)
        {
            for (int i = 0; i < a_Updates.Count; i++)
                m_Vue.AjouterRow();

            int t_currentIndex = 0;
            foreach (CsGoUpdate t_update in a_Updates) {
                CsUpdateBtn t_upBtn = new CsUpdateBtn(t_update, t_currentIndex);
                m_Vue.AfficherUpdate(t_upBtn);
                t_currentIndex++;
            }
        }
    }
}
