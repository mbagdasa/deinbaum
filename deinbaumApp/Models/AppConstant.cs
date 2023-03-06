using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Maui.Behaviors;
using deinbaumApp.Controls;
using deinbaumApp.Views.Dashboard;
using Microsoft.Maui.Controls;
using UraniumUI.Material.Controls;

namespace deinbaumApp.Models
{
    /// <summary>
    /// Klasse dient dazu, dynamisch das FlyoutItem zusammenzubauen
    /// </summary>
    public class AppConstant
    {
        /// <summary>
        /// Funktion um die FlyoutItems rollenbasiert zu erstellen
        /// Admin Berechtigte User haben mehr Flyoutitems zur Verfuegung
        /// </summary>
        /// <returns>Task</returns>
        public async static Task AddFlyoutMenusDetails()
        {
            AppShell.Current.FlyoutHeader = new FlyoutHeaderControl();

            var flyoutItemBaumItem = AppShell.Current.Items.Where(f => f.Route.Equals("FlyoutItemBaum")).FirstOrDefault();
            if (flyoutItemBaumItem != null) AppShell.Current.Items.Remove(flyoutItemBaumItem);

            var flyoutItemMitarbeiterItem = AppShell.Current.Items.Where(f => f.Route.Equals("FlyoutItemMitarbeiter")).FirstOrDefault();
            if (flyoutItemMitarbeiterItem != null) AppShell.Current.Items.Remove(flyoutItemMitarbeiterItem);

            var flyoutItemWaldeigentuemerItem = AppShell.Current.Items.Where(f => f.Route.Equals("FlyoutItemWaldeigentuemer")).FirstOrDefault();
            if (flyoutItemWaldeigentuemerItem != null) AppShell.Current.Items.Remove(flyoutItemWaldeigentuemerItem);

            var flyoutItemVerwaltungItem = AppShell.Current.Items.Where(f => f.Route.Equals("FlyoutItemVerwaltung")).FirstOrDefault();
            if (flyoutItemVerwaltungItem != null) AppShell.Current.Items.Remove(flyoutItemVerwaltungItem);

            var flyoutItemOthersItem = AppShell.Current.Items.Where(f => f.Route.Equals("FlyoutItemOthers")).FirstOrDefault();
            if (flyoutItemOthersItem != null) AppShell.Current.Items.Remove(flyoutItemOthersItem);


            // FlyoutItems thematisch erstellen
            var flyoutItemBaum = new FlyoutItem()
            {

                Title = "FlyoutItemBaum",
                Route = "FlyoutItemBaum",
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                Items =
                    {
                        new ShellContent
                        {
                            Title = "Baum",
                            Icon = "tree_icon.png",
                            Route= nameof(BaumPage),
                            ContentTemplate = new DataTemplate(typeof(BaumPage)),
                        },
                        new ShellContent
                        {
                            Title = "Bäume",
                            Icon = "trees_icon.png",
                            Route= nameof(BaeumePage),
                            ContentTemplate = new DataTemplate(typeof(BaeumePage)),
                        }
                   }
            };

            var flyoutItemMitarbeiter = new FlyoutItem()
            {

                Title = "FlyoutItemMitarbeiter",
                Route = "FlyoutItemMitarbeiter",
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                Items =
                    {
                        new ShellContent
                        {
                            Title = "Feldmitarbeiter",
                            Icon = "feldmitarbeiter_liste.png",
                            Route= nameof(MitarbeiterPage),
                            ContentTemplate = new DataTemplate(typeof(MitarbeiterPage)),
                        },
                        new ShellContent
                        {
                            Title = "Feldmitarbeiter erfassen",
                            Icon = "feldmitarbeiter_add.png",
                            Route= nameof(MitarbeiterRegisterPage),
                            ContentTemplate = new DataTemplate(typeof(MitarbeiterRegisterPage)),
                        }
                   }
            };

            var flyoutItemWaldeigentuemer = new FlyoutItem()
            {

                Title = "FlyoutItemWaldeigentuemer",
                Route = "FlyoutItemWaldeigentuemer",
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                Items =
                    {
                        new ShellContent
                        {
                            Title = "Waldeigentümer",
                            Icon = "waldeigentuemer_liste.png",
                            Route= nameof(WaldeigentuemerPage),
                            ContentTemplate = new DataTemplate(typeof(WaldeigentuemerPage)),
                        },
                        new ShellContent
                        {
                            Title = "Waldeigentümer erfassen",
                            Icon = "waldeigentuemer_add.png",
                            Route= nameof(WaldeigentuemerRegisterPage),
                            ContentTemplate = new DataTemplate(typeof(WaldeigentuemerRegisterPage)),
                        }
                   }
            };

            var flyoutItemVerwaltung = new FlyoutItem()
            {

                Title = "FlyoutItemVerwaltung",
                Route = "FlyoutItemVerwaltung",
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                Items =
                    {
                        new ShellContent
                        {
                            Title = "Baumart verwalten",
                            Icon = "trees_icon.png",
                            Route= nameof(BaumArtVerwaltungPage),
                            ContentTemplate = new DataTemplate(typeof(BaumArtVerwaltungPage)),
                        }
                   }
            };

            var flyoutItemOthers = new FlyoutItem()
            {

                Title = "FlyoutItemOthers",
                Route = "FlyoutItemOthers",
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                Items =
                    {
                        new ShellContent
                        {
                            Title = "Über",
                            Icon = "icon_about.png",
                            Route= nameof(AboutPage),
                            ContentTemplate = new DataTemplate(typeof(AboutPage)),
                        }
                   }
            };

            var flyoutItemWindowsAdmin = new FlyoutItem()
            {

                Title = "FlyoutItemOthers",
                Route = "FlyoutItemOthers",
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                Items =
                    {
                        new ShellContent
                        {
                            Title = "Baum",
                            Icon = "tree_icon.png",
                            Route= nameof(BaumPage),
                            ContentTemplate = new DataTemplate(typeof(BaumPage)),
                        },
                        new ShellContent
                        {
                            Title = "Bäume",
                            Icon = "trees_icon.png",
                            Route= nameof(BaeumePage),
                            ContentTemplate = new DataTemplate(typeof(BaeumePage)),
                        },
                        new ShellContent
                        {
                            Title = "Feldmitarbeiter",
                            Icon = "feldmitarbeiter_liste.png",
                            Route= nameof(MitarbeiterPage),
                            ContentTemplate = new DataTemplate(typeof(MitarbeiterPage)),
                        },
                        new ShellContent
                        {
                            Title = "Feldmitarbeiter erfassen",
                            Icon = "feldmitarbeiter_add.png",
                            Route= nameof(MitarbeiterRegisterPage),
                            ContentTemplate = new DataTemplate(typeof(MitarbeiterRegisterPage)),
                        },
                        new ShellContent
                        {
                            Title = "Waldeigentümer",
                            Icon = "waldeigentuemer_liste.png",
                            Route= nameof(WaldeigentuemerPage),
                            ContentTemplate = new DataTemplate(typeof(WaldeigentuemerPage)),
                        },
                        new ShellContent
                        {
                            Title = "Waldeigentümer erfassen",
                            Icon = "waldeigentuemer_add.png",
                            Route= nameof(WaldeigentuemerRegisterPage),
                            ContentTemplate = new DataTemplate(typeof(WaldeigentuemerRegisterPage)),
                        },
                        new ShellContent
                        {
                            Title = "Über",
                            Icon = "icon_about.png",
                            Route= nameof(AboutPage),
                            ContentTemplate = new DataTemplate(typeof(AboutPage)),
                        }
                   }
            };

            var flyoutItemWindows = new FlyoutItem()
            {

                Title = "FlyoutItemOthers",
                Route = "FlyoutItemOthers",
                FlyoutDisplayOptions = FlyoutDisplayOptions.AsMultipleItems,
                Items =
                    {
                        new ShellContent
                        {
                            Title = "Baum",
                            Icon = "tree_icon.png",
                            Route= nameof(BaumPage),
                            ContentTemplate = new DataTemplate(typeof(BaumPage))
                        },
                        new ShellContent
                        {
                            Title = "Bäume",
                            Icon = "trees_icon.png",
                            Route= nameof(BaeumePage),
                            ContentTemplate = new DataTemplate(typeof(BaeumePage))
                        },
                        new ShellContent
                        {
                            Title = "Über",
                            Icon = "icon_about.png",
                            Route= nameof(AboutPage),
                            ContentTemplate = new DataTemplate(typeof(AboutPage))
                        }
                   }
            };

            // FlyoutItems je nach Berechtigung anzeigen
            if (App.UserDetails.IstAdminBerechtigt)
            {
                if (DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    if (!AppShell.Current.Items.Contains(flyoutItemWindowsAdmin))
                    {
                        AppShell.Current.Items.Add(flyoutItemWindowsAdmin);
                    }
                }
                else
                {
                    if (!AppShell.Current.Items.Contains(flyoutItemBaum))
                    {
                        AppShell.Current.Items.Add(flyoutItemBaum);
                    }

                    if (!AppShell.Current.Items.Contains(flyoutItemMitarbeiter))
                    {
                        AppShell.Current.Items.Add(flyoutItemMitarbeiter);
                    }

                    if (!AppShell.Current.Items.Contains(flyoutItemWaldeigentuemer))
                    {
                        AppShell.Current.Items.Add(flyoutItemWaldeigentuemer);
                    }

                    // TODO: Die Pages und die Logik um auch die Baumarten etc zu verwalten muss noch erstellt werden
                    //if (!AppShell.Current.Items.Contains(flyoutItemVerwaltung))
                    //{
                    //    AppShell.Current.Items.Add(flyoutItemVerwaltung);
                    //}

                    if (!AppShell.Current.Items.Contains(flyoutItemOthers))
                    {
                        AppShell.Current.Items.Add(flyoutItemOthers);
                    }
                }
            }
            else
            {
                if (DeviceInfo.Platform == DevicePlatform.WinUI)
                {
                    if (!AppShell.Current.Items.Contains(flyoutItemWindows))
                    {
                        AppShell.Current.Items.Add(flyoutItemWindows);
                    }
                }
                else
                {
                    if (!AppShell.Current.Items.Contains(flyoutItemBaum))
                    {
                        AppShell.Current.Items.Add(flyoutItemBaum);
                    }

                    if (!AppShell.Current.Items.Contains(flyoutItemOthers))
                    {
                        AppShell.Current.Items.Add(flyoutItemOthers);
                    }
                }
            }

            if (DeviceInfo.Platform == DevicePlatform.WinUI)
            {
                AppShell.Current.Dispatcher.Dispatch(async () =>
                {
                    await Shell.Current.GoToAsync($"//FlyoutItemOthers/{nameof(AboutPage)}");
                });
            }
            else
            {
                await Shell.Current.GoToAsync($"//FlyoutItemOthers/{nameof(AboutPage)}");
            }
        }
    }
}
