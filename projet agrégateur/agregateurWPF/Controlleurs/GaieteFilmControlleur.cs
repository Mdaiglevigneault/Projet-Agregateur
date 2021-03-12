using System.Collections.Generic;
using agregateurWPF;

namespace agregateurMetier
{
    public class GaieteFilmControlleur
    {
        private CinePage m_Vue;
        private int m_Column = 2;

        public GaieteFilmControlleur(CinePage a_Vue) {
            m_Vue = a_Vue;
            GenerateFilmButtons(MainWindowControlleur.Instance.Representations);
        }

        private void GenerateFilmButtons(List<GaieteFilm> a_films) {
            int t_NbRow = (float)a_films.Count / m_Column > (a_films.Count / m_Column) ? (a_films.Count/ m_Column) + 1 : (a_films.Count / m_Column);
            for (int i = 0; i < t_NbRow; i++)
                m_Vue.AjouterRow();

            GaieteFilm[] t_GaieteArray = a_films.ToArray();
            int t_currentIndex = 0;
            for (int i = 0; i < t_NbRow; i++) {
                for (int j = 0; j < m_Column; j++) {
                    if (t_GaieteArray[t_currentIndex] != null) {
                        Film t_vue = new Film(t_GaieteArray[t_currentIndex], i, j);
                        m_Vue.AfficherFilm(t_vue);
                        t_currentIndex++;
                    }
                }
            }
        }
    }
}
