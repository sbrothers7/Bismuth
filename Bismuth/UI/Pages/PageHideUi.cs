using UnityEngine;

namespace Bismuth.UI.Pages
{
    internal static class PageHideUi
    {
        public static void Build(RectTransform content)
        {
            var s = UICore.Settings;
            var notify = UICore.OnSettingsChanged;

            UIBuilder.SectionHeader(content, "Hide UI");
            UIBuilder.Collapsible(content, "Enable", s.HideUiEnabled,
                v => { s.HideUiEnabled = v; notify?.Invoke(); }, null);

            // Forward-declare the sub-container so the Hide All toggle handler can flip its
            // visibility. When Hide All is on, the individual toggles are no-ops, so we hide
            // them entirely — matches the IMGUI version's conditional render.
            GameObject subContainer = null;

            UIBuilder.Collapsible(content, "Hide all UI", s.HideAllUI,
                v =>
                {
                    s.HideAllUI = v;
                    if (subContainer != null) subContainer.SetActive(!v);
                    notify?.Invoke();
                }, null);

            UIBuilder.Spacer(content);
            UIBuilder.SectionHeader(content, "Individual");

            subContainer = UIBuilder.Rect("HideSubs", content);
            var vlg = subContainer.AddComponent<UnityEngine.UI.VerticalLayoutGroup>();
            vlg.childControlWidth = true;
            vlg.childControlHeight = true;
            vlg.childForceExpandWidth = true;
            vlg.childForceExpandHeight = false;
            vlg.spacing = 2f;

            UIBuilder.Collapsible(subContainer.transform, "Hit error meter", s.HideHitmeter,
                v => { s.HideHitmeter = v; notify?.Invoke(); }, null);

            UIBuilder.Collapsible(subContainer.transform, "Autoplay text", s.HideAutoplayText,
                v => { s.HideAutoplayText = v; notify?.Invoke(); }, null);

            UIBuilder.Collapsible(subContainer.transform, "Autoplay icon", s.HideAutoplayIcon,
                v => { s.HideAutoplayIcon = v; notify?.Invoke(); }, null);

            UIBuilder.Collapsible(subContainer.transform, "No-Fail", s.HideNoFail,
                v => { s.HideNoFail = v; notify?.Invoke(); }, null);

            UIBuilder.Collapsible(subContainer.transform, "Difficulty", s.HideDifficulty,
                v => { s.HideDifficulty = v; notify?.Invoke(); }, null);

            // Inline header toggle enables/disables the feature. Body: a master "Hide all
            // judgements" toggle at top that, when on, hides the per-category rows (they're
            // moot) — same forward-declared-subcontainer pattern as the page's Hide all UI.
            UIBuilder.Collapsible(subContainer.transform, "Hide judgements", s.HideJudgementsEnabled,
                v => { s.HideJudgementsEnabled = v; notify?.Invoke(); },
                body =>
                {
                    GameObject catContainer = null;

                    UIBuilder.Collapsible(body, "Hide all judgements", s.HideJudgementsAll,
                        v =>
                        {
                            s.HideJudgementsAll = v;
                            if (catContainer != null) catContainer.SetActive(!v);
                            notify?.Invoke();
                        }, null);

                    catContainer = UIBuilder.Rect("JudgementCats", body);
                    var catVlg = catContainer.AddComponent<UnityEngine.UI.VerticalLayoutGroup>();
                    catVlg.childControlWidth = true;
                    catVlg.childControlHeight = true;
                    catVlg.childForceExpandWidth = true;
                    catVlg.childForceExpandHeight = false;
                    catVlg.spacing = 2f;

                    UIBuilder.Collapsible(catContainer.transform, "Perfects", s.HideJudgementsPerfect,
                        v => { s.HideJudgementsPerfect = v; notify?.Invoke(); }, null);
                    UIBuilder.Collapsible(catContainer.transform, "E/LPerfects", s.HideJudgementsELPerfect,
                        v => { s.HideJudgementsELPerfect = v; notify?.Invoke(); }, null);
                    UIBuilder.Collapsible(catContainer.transform, "Early/Late", s.HideJudgementsEarlyLate,
                        v => { s.HideJudgementsEarlyLate = v; notify?.Invoke(); }, null);
                    UIBuilder.Collapsible(catContainer.transform, "Misses", s.HideJudgementsMiss,
                        v => { s.HideJudgementsMiss = v; notify?.Invoke(); }, null);
                    UIBuilder.Collapsible(catContainer.transform, "Deaths", s.HideJudgementsDeath,
                        v => { s.HideJudgementsDeath = v; notify?.Invoke(); }, null);

                    catContainer.SetActive(!s.HideJudgementsAll);
                });

            UIBuilder.Collapsible(subContainer.transform, "Song title", s.HideLevelName,
                v => { s.HideLevelName = v; notify?.Invoke(); }, null);

            UIBuilder.Collapsible(subContainer.transform, "Alpha/beta build text", s.HideBetaBuild,
                v => { s.HideBetaBuild = v; notify?.Invoke(); }, null);

            subContainer.SetActive(!s.HideAllUI);
        }
    }
}
