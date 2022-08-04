using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.SavedFiles
{
    public static class SavedFileModel
    {
        public class Add<T>
        {
            /// <summary>
            /// Файл
            /// </summary>
            [Required]
            public T File { get; set; }
        }

        public class IdHasBase

        {
            /// <summary>
            /// Код
            /// </summary>
            [Required]
            public int Id { get; set; }
        }

        public class Edit<T> : IdHasBase
        {
            /// <summary>
            /// Новый файл
            /// </summary>
            [Required]
            public T File { get; set; }
        }

        public class Get : IdHasBase
        {
            /// <summary>
            /// Ссылка на файл
            /// </summary>
            public string Link { get; set; }
        }

        public class Save
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Path { get; set; }
            public int MarathonId { get; set; }
        }
    }

}
