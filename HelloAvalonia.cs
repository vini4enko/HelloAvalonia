using HostMgd.EditorInput;
using HostMgd.ApplicationServices;
using Teigha.DatabaseServices;
using Teigha.Geometry;
using Teigha.Runtime;
using Teigha.DatabaseServices;
using Teigha.Runtime;
using Avalonia.Controls;

namespace HelloAvalonia
{
    public class Examples
    {
        static Document doc = HostMgd.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
        static Editor ed = doc.Editor;
        static Database db = doc.Database;

        [CommandMethod("HelloAvalonia")]
        public void HelloAvalonia()
        {
            WindowExample window = new WindowExample();
            HostMgdAvalonia.ApplicationServices.Application.ShowModalWindow(window);
        }

        public static string GetPoint()
        {
            string msg = "Выбор отменен";
            PromptPointResult result = ed.GetPoint("Выберите точку:");
            if (result.Status == PromptStatus.OK)
            {
                msg = $"X: {result.Value.X:F3}, Y: {result.Value.Y:F3}";
            }
            return msg;
        }

        [CommandMethod("HelloAvaloniaPalette")]
        public void HelloAvaloniaPalette()
        {
            var paletteSet = new HostMgdAvalonia.Windows.PaletteSet(("Avalonia: Счётчик примитивов"));
            var paletteControl = new PaletteExample();
            paletteSet.Add("Счётчик", paletteControl);
            paletteSet.Focus();// На случай, если боковая панель скрыта
            paletteSet.Visible = true;// На случай, если уже закрыли этот набор
        }

        public static Dictionary<string, int> GetCounts()
        {
            // Словарь для подсчета
            var counts = new Dictionary<string, int>();

            using (var tr = db.TransactionManager.StartTransaction())
            {
                // Получаем пространство модели
                var modelSpace = (BlockTableRecord)tr.GetObject(
                    ((BlockTable)tr.GetObject(db.BlockTableId, OpenMode.ForRead))[BlockTableRecord.ModelSpace],
                    OpenMode.ForRead
                );

                // Подсчет примитивов
                foreach (ObjectId id in modelSpace)
                {
                    var entity = tr.GetObject(id, OpenMode.ForRead) as Entity;
                    if (entity == null) continue;

                    string type = entity.GetType().Name;
                    counts[type] = counts.ContainsKey(type) ? counts[type] + 1 : 1;
                }
                tr.Commit();
            }
            return counts;
        }
    }
}
